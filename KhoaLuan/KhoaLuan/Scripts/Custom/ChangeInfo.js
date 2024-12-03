$(document).ready(function () {
    var birthday = $('#birthday').val();
    var newBirthday = "";
    var sex = $('#sex').val();
    var provinceID = $('#provinceID').html();
    var districtID;

    var editNameClick = false;
    var editBirthdayClick = false;
    var editPhoneNumberClick = false;
    var editSexClick = false;
    var editProvinceClick = false;
    var editDistrictClick = false;

    var result = false;    

    $('#editName').click(function () {
        editNameClick = !editNameClick;
        var name = $('#userName').val();
        if (editNameClick == true) {
            $('#userName').addClass('my-form-control');
            $('#userName').removeAttr('readonly');
            $('#editName').css({ 'color': 'green' });
            $('#editName1').css({ 'display': 'none' });
            $('#editName2').css({ 'display': '-webkit-inline-box' });
        } else {
            if (name.length == 0) {
                editNameClick = true;
                $('#userName').removeClass('my-form-control');
                $('#userName').addClass('my-form-control-error');
                $('#userName').focus();
            } else {
                $('#userName').removeClass('my-form-control');
                $('#userName').removeClass('my-form-control-error');
                $('#userName').attr('readonly', '');
                $('#editName').css({ 'color': 'black' });
                $('#editName1').css({ 'display': '-webkit-inline-box' });
                $('#editName2').css({ 'display': 'none' });
            }
        }
    });
   
    $('#editBirthday').click(function () {
        newBirthday = $('#birthday').val();
        editBirthdayClick = !editBirthdayClick;
        if (editBirthdayClick == true) {
            $('#birthday').addClass('my-form-control');
            $('#birthday').removeAttr('readonly');
            $('#birthday').attr('type', 'date');
            $('#editBirthday').css({ 'color': 'green' });
            $('#editBirthday1').css({ 'display': 'none' });
            $('#editBirthday2').css({ 'display': '-webkit-inline-box' });
        } else {
            if (newBirthday != "") {
                $('#birthday').val(newBirthday);
                $('#birthday').attr('readonly', '');
                $('#birthday').attr('type', 'text');
                $('#editBirthday').css({ 'color': 'black' });
                $('#editBirthday1').css({ 'display': '-webkit-inline-box' });
                $('#editBirthday2').css({ 'display': 'none' });
                $('#birthday').removeClass('my-form-control');
                $('#birthday').removeClass('my-form-control-error');
            } else {
                editBirthdayClick = true;
                $('#birthday').removeClass('my-form-control');
                $('#birthday').addClass('my-form-control-error');
                $('#birthday').focus();
            }
            
        }
    });

    $('#editPhoneNumber').click(function () {
        editPhoneNumberClick = !editPhoneNumberClick;
        var phone = $('#phone').val();
        if (editPhoneNumberClick == true) {
            $('#phone').addClass('my-form-control');
            $('#phone').removeAttr('readonly');
            $('#editPhoneNumber').css({ 'color': 'green' });
            $('#editPhoneNumber1').css({ 'display': 'none' });
            $('#editPhoneNumber2').css({ 'display': '-webkit-inline-box' });
        } else {
            if (phone.length == 0) {
                editPhoneNumberClick = true;
                $('#phone').removeClass('my-form-control');
                $('#phone').addClass('my-form-control-error');
                $('#phone').focus();
            } else {
                $('#phone').attr('readonly', '');
                $('#editPhoneNumber').css({ 'color': 'black' });
                $('#editPhoneNumber1').css({ 'display': '-webkit-inline-box' });
                $('#editPhoneNumber2').css({ 'display': 'none' });
                $('#phone').removeClass('my-form-control');
                $('#phone').removeClass('my-form-control-error');
            }
        }
    });

    $('#editSex').click(function () {
        sex = $('#sex').val();
        editSexClick = !editSexClick;
        if (editSexClick == true) {
            $('#sex').addClass('my-form-control');
            $('#sex').removeAttr('readonly');
            $('#sex').html('');
            $('#sex').html("<option value='1'>Nam</option><option value='2'>Nữ</option>");
            $('#editSex').css({ 'color': 'green' });
            $('#editSex1').css({ 'display': 'none' });
            $('#editSex2').css({ 'display': '-webkit-inline-box' });
        } else {
            $('#sex').removeClass('my-form-control');
            $('#sex').attr('readonly', '');
            $('#sex').html('');
            if (sex == 1) {
                $('#sex').html("<option value='1'>Nam</option>");
            } else {
                $('#sex').html("<option value='2'>Nữ</option>");
            }
            $('#editSex').css({ 'color': 'black' });
            $('#editSex1').css({ 'display': '-webkit-inline-box' });
            $('#editSex2').css({ 'display': 'none' });
        }
    });

    $('#editProvince').click(function () {
        editProvinceClick = !editProvinceClick;
        if (editProvinceClick == true) {
            $('#province').removeClass('my-form-control');
            var listProvince = loadProvince();
            $('#province').removeAttr('readonly');
            $('#province').html('');
            $('#province').html(listProvince);
            $('#editProvince').css({ 'color': 'green' });
            $('#editProvince1').css({ 'display': 'none' });
            $('#editProvince2').css({ 'display': '-webkit-inline-box' });

            $('#district').removeAttr('readonly');
            $('#editDistrict').css({ 'color': 'green' });
            $('#editDistrict1').css({ 'display': 'none' });
            $('#editDistrict2').css({ 'display': '-webkit-inline-box' });
            
        } else {
            $('#province').removeClass('my-form-control');
            var ProvinceName = loadProvinceName(provinceID);
            $('#province').html('');
            $('#province').html("<option value='" + provinceID + "'>" + ProvinceName + "</option>");
            $('#province').attr('readonly', '');
            $('#editProvince').css({ 'color': 'black' });
            $('#editProvince1').css({ 'display': '-webkit-inline-box' });
            $('#editProvince2').css({ 'display': 'none' });
        }
    });

    $('#editDistrict').click(function () {
        districtID = $('#district').val();
        editDistrictClick = !editDistrictClick;
        if (editDistrictClick == true) {
            $('#district').removeClass('my-form-control');
            provinceID = $('#province').val();
            var listDistrict = loadDistrict(provinceID);
            $('#district').html('');
            $('#district').html(listDistrict);
            $('#district').removeAttr('readonly');
            $('#editDistrict').css({ 'color': 'green' });
            $('#editDistrict1').css({ 'display': 'none' });
            $('#editDistrict2').css({ 'display': '-webkit-inline-box' });

        } else {
            $('#district').removeClass('my-form-control');
            var districtName = loadDistrictName(districtID);
            $('#district').html('');
            $('#district').html("<option value='" + districtID + "'>" + districtName + "</option>");
            $('#district').attr('readonly', '');
            $('#editDistrict').css({ 'color': 'black' });
            $('#editDistrict1').css({ 'display': '-webkit-inline-box' });
            $('#editDistrict2').css({ 'display': 'none' });
        }
    });

    $('#province').change(function () {
        provinceID = $('#province').val();
        var listDistrict = loadDistrict(provinceID);
        $('#district').html('');
        $('#district').html(listDistrict);
    });

    $('#district').change(function () {
        districtID = $('#district').val();
    });

    $('#saveInfo').click(function () {
        var accountID = $('#accountID').val();
        var nameUser = $('#userName').val();
        var birthday = $('#birthday').val();
        var phone = $('#phone').val();
        var sex = $('#sex').val();
        var province = $('#province').val();
        var district = $('#district').val();
        $.ajax({
            async: false,
            type: "POST",
            url: "/Account/ChangeInfo",
            data: {
                accountID: accountID,
                nameUser: nameUser,
                birthday: birthday,
                phone: phone,
                sex: sex,
                province: province,
                district: district
            },
            dataType: "JSON",
            success: function (response) {
                $('#notificationModal').modal('show');
                $('#contentNotification').html("Đã lưu thay đổi");
            }
        });
    });

    setInterval(function () {
        var k = false;
        if (!Number.isNaN(districtID)) {
            k = true;
        }
        result = !editNameClick && !editBirthdayClick && !editPhoneNumberClick && !editSexClick && !editProvinceClick && !editDistrictClick &&k;
        if (!result) {
            $('#saveInfo').attr('disabled', 'true');
        } else {
            $('#saveInfo').removeAttr('disabled');
        }
    }, 500);

    function loadProvince() {
        var listProvince;
        $.ajax({
            async: false,
            type: "POST",
            url: "/Address/LoadProvince",
            dataType: "JSON",
            success: function (response) {
                listProvince = response.result;
            }
        });
        return listProvince;
    }

    function loadProvinceName(provinceID) {
        var provinceName = "";
        $.ajax({
            async: false,
            type: "POST",
            url: "/Address/LoadProvinceName",
            data: { provinceID: provinceID },
            dataType: "JSON",
            success: function (response) {
                provinceName = response.result;
            }
        });
        return provinceName;
    }

    function loadDistrict(provinceID) {
        var listDistrictOfprovince;
        $.ajax({
            async: false,
            type: "POST",
            url: "/Address/LoadDistrict",
            data: { provinceID: provinceID },
            dataType: "JSON",
            success: function (response) {
                listDistrictOfprovince = response.result;
            }
        });
        return listDistrictOfprovince;
    }

    function loadDistrictName(districtID) {
        var districtName;
        $.ajax({
            async: false,
            type: "POST",
            url: "/Address/LoadDistrictName",
            data: { districtID: districtID },
            dataType: "JSON",
            success: function (response) {
                districtName = response.result;
            }
        });
        return districtName;
    }
});