$(function () {
    // Initialize all modals once
    const modals = {
        delete: new bootstrap.Modal('#deleteModal')
    };

    // Delete Booking handling
    $(document).on('click', '.delete-equipment-btn', function () {
        const bookingId = $(this).data('id');
        const bookingName = $(this).closest('tr').find('td:first').text();

        $('#bookingName').text(bookingName);
        $('#deleteBookingId').val(bookingId);
        $('#bookingType').val('EQUIP');
        modals.delete.show();
    });

    $(document).on('click', '.delete-court-btn', function () {
        const bookingId = $(this).data('id');
        const bookingName = $(this).closest('tr').find('td:first').text();

        $('#bookingName').text(bookingName);
        $('#deleteBookingId').val(bookingId);
        $('#bookingType').val('COURT');
        modals.delete.show();
    });


    $('#confirmDelete').click(async function () {
        const bookingId = $('#deleteBookingId').val();
        const bookinType = $('#bookingType').val();
        const $btn = $(this).prop('disabled', true);
        var actionDelete = null;

        try {
            switch (bookinType) {
                case "COURT":
                    actionDelete = "DeleteCourtBooking";
                    break;
                case "EQUIP":
                    actionDelete = "DeleteEquipmentBooking";
                    break;
                default:
                    break;
            }
                
                const response = await fetch(`/Student/${actionDelete}/${bookingId}`, {
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
            showToast('Failed to delete booking. Please try again.', 'danger');
        } finally {
            $btn.prop('disabled', false);
        }
    });
});

// Toast notification function
function showToast(message, type) {
    const toast = bootstrap.Toast.getOrCreateInstance('#statusToast');
    $('#toastMessage').text(message);
    $('#statusToast').removeClass('bg-success bg-danger').addClass(`bg-${type}`);
    toast.show();
}