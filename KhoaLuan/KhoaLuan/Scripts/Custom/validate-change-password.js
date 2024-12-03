$(document).ready(function () {
    $('#btnChangePassword').click(function () {
        var accountID = $('#accountID');
        var currentPassword = $('#currentPassword');
        var newPassword = $('#newPassword');
        var newPasswordConfirm = $('#newPasswordConfirm');
        var valid1 = validateCurrentPass(accountID, currentPassword);
        var valid2 = validateNewPass(newPassword);
        var valid3 = validateNewPassConfirm(newPasswordConfirm);
        var valid4 = false;
        if (valid2 == true && valid3 == true) {
            valid4 = validatePassConfirm(newPassword, newPasswordConfirm);
        }
        if (valid1 && valid2 && valid3 && valid4) {
            changePassword(accountID, currentPassword, newPasswordConfirm);
        }
    });
});
function showMessError(idError, contentError, selector) {
    $('#' + idError).html(contentError);
    selector.focus();
}
function hideMessError(idError) {
    $('#' + idError).html("");
}

function validateCurrentPass(accountID, currentPassword) {
    var accID = accountID.val();
    var pass = currentPassword.val();
    var flag = false;
    if (currentPassword.val() == "") {
        showMessError("error-current-password", "Phải nhập mật khẩu hiện tại", currentPassword);
    }
    else {
        $.ajax({
            async: false,
            type: "POST",
            url: "/Account/CheckCurrentPassword",
            data: {
                accountID: accID,
                currentPassword: pass
            },
            dataType: "JSON",
            success: function (response) {
                if (response.result) {
                    hideMessError("error-current-password");
                    flag = true;
                } else {
                    showMessError("error-current-password", "Mật khẩu không chính xác", currentPassword);
                }
            }
        });
    }
    return flag;
}

function validateNewPass(newPassword) {
    var pass = newPassword.val();
    if (pass == "") {
        showMessError("error-new-password", "Phải nhập mật khẩu mới", newPassword);
        return false;
    }
    hideMessError("error-new-password");
    return true;
}

function validateNewPassConfirm(newPasswordConfirm) {
    var pass = newPasswordConfirm.val();
    if (pass == "") {
        showMessError("error-new-password-confirm", "Phải nhập mật khẩu xác nhận", newPasswordConfirm);
        return false;
    }
    hideMessError("error-new-password-confirm");
    return true;
}
function validatePassConfirm(newPass, newPassConfirm) {
    var pass1 = newPass.val(); var pass2 = newPassConfirm.val();
    if (pass1 == pass2) {
        hideMessError("error-new-password-confirm");
        return true;
    } else {
        showMessError("error-new-password-confirm", "Mật khẩu xác nhận không trùng khớp", newPasswordConfirm);
        return false;
    }
}

function changePassword(accountID, currentPass, newPass) {
    var acc = accountID.val(); var pass1 = currentPass.val(); var pass2 = newPass.val();
    $.ajax({
        async: false,
        type: "POST",
        url: "/Account/ChangePassWord",
        data: {
            accountID: acc,
            currentPassword: pass1,
            newPassword: pass2
        },
        dataType: "JSON",
        success: function (response) {
            if (response.success) {
                $('#contentNotification').html("Thay đổi mật khẩu thành công!!!");
                $('#notificationModal').modal('show');
            }
        }
    });
}