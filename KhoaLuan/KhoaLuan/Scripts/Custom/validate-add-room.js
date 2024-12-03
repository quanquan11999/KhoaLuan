var motelNameAdd = $('#motelNameAdd'),
    priceAdd = $('#priceAdd'),
    provinceAdd = $('#provinceAdd'),
    districtAdd = $('#districtAdd'),
    wardAdd = $('#wardAdd'),
    type = $('#type'),
    acreageAdd = $('#acreageAdd'),
    maxPeopleAdd = $('#maxPeopleAdd'),
    addressRoomAdd = $('#addressRoomAdd');

var formatter = new Intl.NumberFormat('vn-VN', {
    style: 'currency',
    currency: 'VND',
    minimumFractionDigits: 0
});

function currencyToNumber(string) {
    var arr = string.split('.');
    var number = '';
    var index = 0;
    for (index = 0; index < arr.length; index++) {
        number = number + arr[index];
    }
    return number;
}

function showMessage(selector) {
    var root = selector.closest('div');
    root.find('span').addClass('visible');
    selector.css({ "border-color": "#ef1b48" });
}

function hideMessage(selector) {
    var root = selector.closest('div');
    root.find('span').removeClass('visible');
    selector.css({ "border-color": "" });
}

function validateDataAddRoom() {
    var validMotelName = validateMotelName();
    var validPrice = validatePrice();
    var validMaxPeople = validateMaxPeople();
    var validProvince = validateProvince();
    var validDistrict = validateDistrict();
    var validType = validateType();
    var validAcreage = validateAcreage();
    var validAddressRoom = validateAddressRoom();
    var validWard = validateWard();

    var result =validPrice && validMaxPeople() && validProvince && validDistrict &&
        validType && validAcreage && validAddressRoom && validWard && validMotelName;
    return result;
}

function validatePrice() {
    if (priceAdd.val().length === 0) {
        showMessage(priceAdd);
        priceAdd.val('');
        return false;
    } else if (currencyToNumber(priceAdd.val()) < 1000) {
        showMessage(priceAdd);
        $('#priceAddErrorMessage').html('<i class="material-icons">error</i> Giá thuê phải không nhỏ hơn 1,000 VNĐ.');
        return false;
    }
    hideMessage(priceAdd);
    
    return true;
}

priceAdd.focusout(function () { validateMaxPeople(); });

function validateProvince() {
    if (priceAdd.val() === '0') {
        showMessage(priceAdd);
        return false;
    }
    hideMessage(priceAdd);
    return true;
}
maxPeopleAdd.focusout(function () { validateMaxPeople(); });

function validateProvince() {
    if (provinceAdd.val() === '0') {
        showMessage(provinceAdd);
        return false;
    }
    hideMessage(provinceAdd);
    return true;
}
provinceAdd.focusout(function () { validateProvince(); });

function validateDistrict() {
    if (districtAdd.val() === '0') {
        showMessage(districtAdd);
        return false;
    }
    hideMessage(districtAdd);
    return true;
}
districtAdd.focusout(function () { validateDistrict(); });

function validateWard() {
    if (wardAdd.val() === '0') {
        showMessage(wardAdd);
        return false;
    }
    hideMessage(wardAdd);
    return true;
}
wardAdd.focusout(function () { validateWard(); });

function validateType() {
    if (type.val() !== '1' && type.val() !== '0') {
        showMessage(type);
        return false;
    }
    hideMessage(type);
    return true;
}
type.focusout(function () { validateType(); });

function validateAcreage() {
    if (acreageAdd.val().length === 0) {
        showMessage(acreageAdd);
        return false;
    } else if (acreageAdd.val() < 5) {
        showMessage(acreageAdd);
        $('#acreageAddErrorMessage').html('<i class="material-icons">error</i> Diện tích phải lớn hơn 5 m<sup>2</sup>.');
        return false;
    }
    hideMessage(acreageAdd);
    return true;
}
acreageAdd.focusout(function () { validateAcreage(); });

function validateAddressRoom() {
    if (addressRoomAdd.val().length === 0) {
        showMessage(addressRoomAdd);
        return false;
    }
    hideMessage(addressRoomAdd);
    return true;
}
addressRoomAdd.focusout(function () { validateAddressRoom(); });

function validateMotelName() {
    if (motelNameAdd.val().length === 0) {
        showMessage(motelNameAdd);
        return false;
    }
    hideMessage(motelNameAdd);
    return true;
}
motelNameAdd.focusout(function () { validateMotelName(); });

provinceAdd.click(function (e) {
    var cID = provinceAdd.val();
    $.ajax({
        type: "POST",
        url: "/Address/LoadDistrict",
        data: { provinceID: cID },
        dataType: "json",
        success: function (response) {
            //console.log(response.result);
            districtAdd.html(response.result);
        }
    });
});

districtAdd.click(function (e) {
    var dis = districtAdd.val();
    //console.log(cID);
    $.ajax({
        type: "POST",
        url: "/Address/LoadWard",
        data: { districtID: dis },
        dataType: "json",
        success: function (response) {
            //console.log(response.result);
            wardAdd.html(response.result);
        }
    });
});

provinceAdd.bind('keyup', function (e) { provinceAdd.trigger('click'); });