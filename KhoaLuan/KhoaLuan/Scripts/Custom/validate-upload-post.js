var motelRoom = $('#selectMotelID'),
    postTitle = $('#postTitle'),
    type = $('#type'),
    description = $('#description'),
    imageList = $('#imageList');

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

function validateDataUploadPost() {
    var validPostTile = validateTitle();
    var validType = validateType();
    var validImageList = validateImageList();
    var validDescription = validateDescription();
    var validSelectRoom = validateSelectRoom();
    var result = validPostTile && validImageList && validDescription && validSelectRoom;
    return result;
}

function validateSelectRoom() {
    if (motelRoom.val() === '0') {
        showMessage(motelRoom);
        return false;
    }
    hideMessage(motelRoom);
    return true;
}
motelRoom.focusout(function () { validateSelectRoom(); });

function validateTitle() {
    if (postTitle.val() === '') {
        showMessage(postTitle);
        return false;
    }
    hideMessage(postTitle);
    return true;
}
postTitle.focusout(function () { validateTitle(); });

function validateType() {
    if (type.val() !== '1' && type.val() !== '0') {
        showMessage(type);
        return false;
    }
    hideMessage(type);
    return true;
}
type.focusout(function () { validateType(); });

function validateImageList() {
    if (imageList.val().length === 0) {
        showMessage(imageList);
        return false;
    }
    hideMessage(imageList);
    return true;
}
imageList.change(function () { validateImageList(); });
imageList.focusout(function () { validateImageList(); });

function validateDescription() {
    if (description.val().length === 0) {
        showMessage(description);
        return false;
    }
    hideMessage(description);
    return true;
}
description.focusout(function () { validateDescription(); });