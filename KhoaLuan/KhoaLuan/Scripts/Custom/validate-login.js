//#####  Validate Login  #####
var firstTime = true;
var checkLogin = false;

$(document).ready(function () {
    $("#userInput").keyup(function () {
        if (!firstTime) validateUser();
    });
    $("#passwordInput").keyup(function () {
        if (!firstTime) validatePass();
    });
});

function validateLogin() {
    firstTime = false;
    return (validateUser() && validatePass() && checkAccount());
}

function validateUser() {
    var user = $("#userInput").val();
    if (user.length <= 0) {
        $("#error-user").addClass('visible');
        $("#error-user").html('<i class="material-icons">error</i> Phải nhập tên tài khoản.');
        $("#userInputLabel").focus();
        return false;
    } else {
        $("#error-user").removeClass('visible');
        $("#error-user").html('');
        return true;
    }
}

function validatePass() {
    var pass = $("#passwordInput").val();
    if (pass.length <= 0) {
        $("#error-pass").addClass('visible');
        $("#error-pass").html('<i class="material-icons">error</i> Phải nhập mật khẩu.');
        $("#passwordInput").focus();
        return false;
    } else {
        $("#error-pass").removeClass('visible');
        $("#error-pass").html('');
        return true;
    }
}

function checkAccount() {
    var user = $("#userInput").val();
    var pass = $("#passwordInput").val();
    var locked = false;
    $.post({
        async: false,
        url: "/Account/CheckAccount",
        data: { userInput: user, passwordInput: pass },
        dataType: "json",
        success: function (response) {
            checkLogin = response.result;
            locked = response.locked;
        }
    });
    if (!checkLogin) {
        $('#error-account-not-found').addClass('visible');
        $('#error-account-not-found').html('<i class="material-icons">error</i> Tên tài khoản hoặc mật khẩu không đúng.');
        return false;
    }
    if (locked) {
        $('#error-account-not-found').addClass('visible');
        $('#error-account-not-found').html('<i class="material-icons">error</i> Tài khoản đã bị khoá.');
        return false;
    }
    return true;
}
