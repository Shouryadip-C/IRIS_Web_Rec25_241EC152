$(function () {
    // Initialize all modals once
    const modals = {
        create: new bootstrap.Modal('#createModal'),
        edit: new bootstrap.Modal('#editModal'),
        delete: new bootstrap.Modal('#deleteModal')
    };

    // Create Equipment
    $('#createEquipmentBtn').click(function () {
        $.get('/Admin/CreateEquipment')
            .done(data => {
                $('#createModalContent').html(data);
                modals.create.show();
            });
    });

    // Edit Equipment (using event delegation)
    $(document).on('click', '.editEquipmentBtn', function () {
        const equipmentId = $(this).data('id');
        $.get(`/Admin/EditEquipment/${equipmentId}`)
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

    // Delete Equipment handling
    $(document).on('click', '.delete-btn', function () {
        const equipmentId = $(this).data('id');
        const equipmentName = $(this).closest('tr').find('td:first').text();

        $('#equipmentName').text(equipmentName);
        $('#deleteEquipmentId').val(equipmentId);
        modals.delete.show();
    });

    $('#confirmDelete').click(async function () {
        const equipmentId = $('#deleteEquipmentId').val();
        const $btn = $(this).prop('disabled', true);

        try {
            const response = await fetch(`/Admin/DeleteEquipment/${equipmentId}`, {
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
            showToast('Failed to delete equipment. Please try again.', 'danger');
        } finally {
            $btn.prop('disabled', false);
        }
    });

    // Search functionality
    $('#searchInput').on('input', function () {
        const term = $(this).val().toLowerCase();
        $('#equipmentTable tbody tr').each(function () {
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