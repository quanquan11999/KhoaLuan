var CMND = $('#CMND');
var motelID = 0;
$(document).ready(function () {
    var renterID = 0;
    $('#addRenterForm').submit(function () {
        CMND = $('#CMND');
        return CheckSex() && CheckID(CMND.val());
    });

    $('.renter').click(function () {
        var rentID = $(this).attr('id'),
            currentID = $('#currentID' + rentID).html(),
            currentName = $('#currentName' + rentID).html(),
            currentSex = $('#currentSex' + rentID).html().trim(),
            currentPhone = $('#currentPhone' + rentID).html(),
            currentNote = $('#currentNote' + rentID).html();
        var birthday = $('#currentBirthday' + rentID).html();
        $('#rentID').val(rentID);
        $('#EditCMND').val(currentID);
        $('#renterEditName').val(currentName);
        if (currentSex == 'Nam') {
            $('#renterEditSex').html("<option value='0'>Chọn giới tính</option><option selected value='1'>Nam</option><option value='2'>Nữ</option>");
        } else if (currentSex == 'Nữ') {
            $('#renterEditSex').html("<option value='0'>Chọn giới tính</option><option value='1'>Nam</option><option selected value='2'>Nữ</option>");
        }
        $('#renterEditBirthDay').val(birthday);
        $('#renterEditPhoneNumber').val(currentPhone);
        $('#rentEditNote').val(currentNote);
    });

    $('#editRenterForm').submit(function () {
        return true;
    });
    $('.del-renter').click(function () {
        renterID = $(this).attr('id');
    });
    $('#del-renter-confirm').click(function () {
        motelID = $('#get-motelID').html();
        $.ajax({
            async: true,
            type: "POST",
            url: "/User/DelRenter",
            data: {
                rentID: renterID,
                moID: motelID
            },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    window.location ="RoomDetails?motelID="+motelID;
                } else {
                    alert("Xoá không thành công");
                }
            }
        });
    });
});

function CheckSex() {
    var sex = document.getElementById('renterSex');
    if (sex.value == 0) {
        sex.style.borderColor = 'red';
        return false;
    }
    sex.style.borderColor = 'black';
    return true;
}


function CheckID(ID) {
    $('#CMNDerror').css({'display':'none'});
    $.ajax({
        async: true,
        type: "POST",
        url: "/User/CheckID",
        data: {
            ID: ID
        },
        dataType: "json",
        success: function (response) {
            if (response.success) {
                $('#CMNDerror').css({ 'display': 'none' });
                return true;
            } else {
                $('#CMNDerror').css({ 'display': 'block' });
                return false;
            }
        }
    });
}
CMND.focusout(function () { CheckID(CMND.val()); });