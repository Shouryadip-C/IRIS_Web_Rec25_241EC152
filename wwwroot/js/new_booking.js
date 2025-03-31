$(document).ready(function () {
    $("#nameError").hide();
    $("#SelectedQuantity").val('');
    getDate();

    // When a court radio button is selected, update the slot dropdown.
    $(".court-radio").change(function () {
        var id = $(this).val();
        var bookingDate = $("#datePicker").val();
        var $slotContainer = $("#CourtSlotContainer");
        var $slotSelect = $("#SelectedTimeSlot");

        if (!id || !bookingDate) {
            alert("Please ensure a court and booking date are selected.");
            return;
        }

        $.ajax({
            url: '@Url.Action("GetAvailableCourtSlots", "Student")',
            type: 'GET',
            data: { id: id, bookingDate: bookingDate },
            success: function (response) {
                $slotContainer.show();
                $slotSelect.empty();
                $slotSelect.append($("<option>", { value: "", text: "-- Select Slot --" }));
                if (response.success) {
                    $.each(response.slots, function (index, slot) {
                        $slotSelect.append($("<option>", { value: slot.hour, text: slot.displayText }));
                    });
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Error retrieving available slots.");
            }
        });
    });

    $(".equipment-radio").change(function () {
        $("#showError").hide();
        $("#SelectedQuantity").val('');
        $("#equipmentAvailableLabel").text('');
        $("#btnSubmit").prop('disabled', true);
    });


    // Copy the selected time slot into the hidden field.
    $("#SelectedTimeSlot").change(function () {
        $("#HiddenSelectedTimeSlot").val($(this).val());
    });

    $("#SelectedQuantity").change(function () {
        $("#HiddenSelectedQuantity").val($(this).val());
    });

    // Copy the selected court id into the hidden field.
    $('input:radio[name="SelectedCourtId"]').click(function () {
        $("#HiddenSelectedCourtId").val($(this).val());
    });

    // Copy the selected equipment id into the hidden field.
    $('input:radio[name="SelectedEquipmentId"]').click(function () {
        $("#HiddenSelectedEquipmentId").val($(this).val());
    });

    $("#SelectedQuantity").on('change keyup paste', function () {
        var opt = $("#SelectedQuantity").val();
        opt = isNaN(parseInt(opt)) ? 0 : parseInt(opt);
        if (opt == 0)
            $("#btnSubmit").prop('disabled', true);
        CheckAvailableEquipment();
    });
});

function getDate() {
    var today = new Date();
    var mindate = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2);
    $("#datePicker").attr('min', mindate);
}

function validate() {
    var bookingType = ("#BookingType").val();
    if (bookingType = "Equipment") {
        var equipmentId = $("input[name='SelectedEquipmentId']:checked").val();
        var bookingDate = $("#datePicker").val();
        var hourSlot = $("#SelectedTimeSlot").val();
        var opt = $("#SelectedQuantity").val();
        opt = isNaN(parseInt(opt)) ? 0 : parseInt(opt);
        if (!equipmentId || !bookingDate || !hourSlot || opt == 0) {
            $("#showError").text('Please select Equipemt, Booking Date and Slot')
            $("#showError").show();
            $("#SelectedQuantity").val('');
            $("#btnSubmit").prop('disabled', true);
            return false;
        }
    }
    return true;
}

function CheckAvailableEquipment() {
    var equipmentId = $("input[name='SelectedEquipmentId']:checked").val();
    var bookingDate = $("#datePicker").val();
    var hourSlot = $("#SelectedTimeSlot").val();
    var opt = parseInt($("#SelectedQuantity").val());
    opt = isNaN(parseInt(opt)) ? 0 : parseInt(opt);

    if (!equipmentId || !bookingDate || !hourSlot) {
        $("#showError").text('\n Please select Equipemt, Booking Date and Slot')
        $("#showError").show();

        $("#SelectedQuantity").val('');
        $("#btnSubmit").prop('disabled', true);
        return false;
    }
    // return new Promise((resolve, reject) => {
    $.ajax({
        url: '@Url.Action("GetEquipmentAvailability", "Student")',
        type: 'GET',
        data: {
            equipmentId: equipmentId,
            bookingDate: bookingDate,
            hourSlot: hourSlot
        },
        success: function (response) {
            if (response.success) {
                $("#equipmentAvailableLabel").text(response.availableQuantity);
                $("input[name='SelectedQuantity']").attr("max", response.availableQuantity);
                if (opt > response.availableQuantity || opt == 0) {
                    $("#SelectedQuantity").addClass("invalid");
                    $("#showError").text("\n Selected quantity is mandatory and should be less than available quantity");
                    $("#showError").show();
                    $("#btnSubmit").prop('disabled', true);
                    return false;
                }
                else {
                    $("#SelectedQuantity").removeClass("invalid");
                    $("#showError").hide();
                    $("#btnSubmit").removeAttr('disabled');
                    return true;
                }
            } else {
                alert(response.message);
                return false;
            }
        },
        error: function (error) {
            alert("Error checking equipment availability.");
            reject(error);
        }
    });
    return false;
};