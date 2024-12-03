var motelID = 0;
var accountID = "";
$(document).ready(function () {
    $('.motel-row').click(function () {
        motelID = $(this).attr('id');
        $('#motelID').html($('#motelID' + motelID).html());
        $('#motelName').html($('#motelName' + motelID).html());
        $('#address').html($('#address' + motelID).html());
        $('#acreage').html($('#acreage' + motelID).html() + ' m<sup>2</sup>');
        $('#price').html(formatter.format($('#price' + motelID).html()).substring(0) + ' (VND)');
        $('#maxPeople').html($('#maxPeople' + motelID).html() + ' người');
        $('#province').html($('#province' + motelID).html());
        $('#district').html($('#district' + motelID).html());
        $('#ward').html($('#ward' + motelID).html());
        $.ajax({
            async: true,
            type: 'POST',
            url: '/Admin/GetCriteria',
            dataType: 'json',
            data: { motelID: motelID },
            success: function (response) {
                if (response.success == true) {
                    $('#acriteria').html(response.result);
                } else {
                    $('#acriteria').html("Không có");
                }
            }
        });
        var statusMotel = $('#motelStatus' + motelID).html();
        if (statusMotel == 2) {
            $('#btnEditRoom').attr('disabled','true');
            $('#btnDeleteRoom').attr('disabled', 'true');
        } else {
            $('#btnEditRoom').removeAttr('disabled');
            $('#btnDeleteRoom').removeAttr('disabled');
        }
    });
});

var motelIDEidt = $('#editMotelID'),
    motelNameEdit = $('#editMotelName'),
    acreageEidt = $('#acreageEidt'),
    priceEdit = $('#priceEdit'),
    maxPeopleEdit = $('#maxPeopleEdit'),
    addressEdit = $('#addressEdit'),
    listCriteria = $('#listCriteria'),
    ward = $('#wardEdit'),
    district = $('#districtEdit'),
    province = $('#provinceEdit');
$(document).ready(function () {
    $('#btnEditRoom').click(function () {
        motelIDEidt.val($('#motelID' + motelID).html());
        motelNameEdit.val($('#motelName' + motelID).html());
        acreageEidt.val($('#acreage' + motelID).html());
        priceEdit.val($('#price' + motelID).html());
        maxPeopleEdit.val($('#maxPeople' + motelID).html());
        addressEdit.val($('#address' + motelID).html());
        listCriteria.html(GetCriteriaForRoom(motelID));

        var districtID = $('#districtID' + motelID).html();
        var provinceID = $('#provinceID' + motelID).html();
        ward.html(GetWardForRoom(motelID, districtID));
        district.html(GetDistrictForRoom(motelID, provinceID));
        province.html(GetProvinceForRoom(motelID));
    });
    $('#del-room-confirm').click(function () {
        $.ajax({
            async: true,
            type: 'POST',
            url: '/User/DelRoom',
            dataType: 'json',
            data: { motelID: motelID },
            success: function (response) {
                if (response.success == true) {
                    window.location = "MotelRooms";
                } else {
                    alert("Xoá không thành công!!!");
                }
            }
        });

    });
    province.change(function () {
        var provinceID = province.val();
        if (provinceID !== null && provinceID != 0) {
            $.ajax({
                type: "POST",
                url: "/Address/LoadDistrict",
                data: { provinceID: provinceID },
                dataType: "json",
                success: function (response) {
                    district.html(response.result);
                }
            });
        }
    });

    district.change(function () {
        var districtID = district.val();
        if (districtID !== null && districtID != 0) {
            $.ajax({
                type: "POST",
                url: "/Address/LoadWard",
                data: { districtID: districtID },
                dataType: "json",
                success: function (response) {
                    ward.html(response.result);
                }
            });
        }
    });
});

function GetCriteriaForRoom(motelID) {
    var result = "";
    $.ajax({
        async: false,
        type: 'POST',
        url: '/User/GetCriteriaForRoom',
        dataType: 'json',
        data: { motelID: motelID },
        success: function (response) {
            if (response.success == true) {
                result = response.result;
            }
        }
    });
    return result;
}

function GetProvinceForRoom(motelID) {
    var result = "";
    $.ajax({
        async: false,
        type: 'POST',
        url: '/Address/GetProvinceForRoom',
        dataType: 'json',
        data: { motelID: motelID },
        success: function (response) {
            if (response.success == true) {
                result = response.result;
            }
        }
    });
    return result;
}
function GetDistrictForRoom(motelID, provinceID) {
    var result = "";
    $.ajax({
        async: false,
        type: 'POST',
        url: '/Address/GetDistrictForRoom',
        dataType: 'json',
        data: {
            motelID: motelID,
            provinceID: provinceID
        },
        success: function (response) {
            if (response.success == true) {
                result = response.result;
            }
        }
    });
    return result;
}
function GetWardForRoom(motelID, districtID) {
    var result = "";
    $.ajax({
        async: false,
        type: 'POST',
        url: '/Address/GetWardForRoom',
        dataType: 'json',
        data: {
            motelID: motelID,
            districtID: districtID
        },
        success: function (response) {
            if (response.success == true) {
                result = response.result;
            }
        }
    });
    return result;
}