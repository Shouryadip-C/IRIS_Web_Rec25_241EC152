$(function () {
    // Initialize all modals once
    const modals = {
        create: new bootstrap.Modal('#createModal'),
        edit: new bootstrap.Modal('#editModal'),
        delete: new bootstrap.Modal('#deleteModal')
    };

    // Create Court
    $('#createCourtBtn').click(function () {
        $.get('/Admin/CreateCourt')
            .done(data => {
                $('#createModalContent').html(data);
                modals.create.show();
            });
    });

    // Edit Court (using event delegation)
    $(document).on('click', '.editCourtBtn', function () {
        const courtId = $(this).data('id');
        $.get(`/Admin/EditCourt/${courtId}`)
            .done(data => {
                $('#editModalContent').html(data);
                modals.edit.show();
            });
    });

    // Unified form submission handler
    $(document).on('submit', '#createForm, #editForm', function (e) {
        e.preventDefault();
        const form = $(this);
        const isEdit = form.attr('id') === 'editForm';

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            headers: {
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    modals[isEdit ? 'edit' : 'create'].hide();
                    window.location.reload();
                } else {
                    $(`#${isEdit ? 'editModal' : 'createModal'}Content`).html(response);
                }
            },
            error: function (xhr) {
                $(`#${isEdit ? 'editModal' : 'createModal'}Content`).html(xhr.responseText);
            }
        });
    });

    // Delete Court handling
    $(document).on('click', '.delete-btn', function () {
        const courtId = $(this).data('id');
        const courtName = $(this).closest('tr').find('td:first').text();

        $('#courtName').text(courtName);
        $('#deleteCourtId').val(courtId);
        modals.delete.show();
    });

    $('#confirmDelete').click(async function () {
        const courtId = $('#deleteCourtId').val();
        const $btn = $(this).prop('disabled', true);

        try {
            const response = await fetch(`/Admin/DeleteCourt/${courtId}`, {
                method: 'POST',
                headers: {
                    'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                }
            });

            if (response.ok) {
                modals.delete.hide();
                window.location.reload();
            } else {
                const error = await response.text();
                showToast('Delete failed: ' + error, 'danger');
            }
        } catch (error) {
            showToast('Failed to delete court. Please try again.', 'danger');
        } finally {
            $btn.prop('disabled', false);
        }
    });

    // Search functionality
    $('#searchInput').on('input', function () {
        const term = $(this).val().toLowerCase();
        $('#courtTable tbody tr').each(function () {
            $(this).toggle($(this).text().toLowerCase().includes(term));
        });
    });
});

// Toast notification function
function showToast(message, type) {
    const toast = bootstrap.Toast.getOrCreateInstance('#statusToast');
    $('#toastMessage').text(message);
    $('#statusToast').removeClass('bg-success bg-danger').addClass(`bg-${type}`);
    toast.show();
}