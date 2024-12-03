$(document).ready(function () {
    var accountId = 0;
    $('.account-row').click(function () {
        accountId = $(this).attr('id');
        $('#btnLockAccount').attr({ 'account-id': accountId });
        $('#btnUnlockAccount').attr({ 'account-id': accountId });
        $('#btnApprovalAccount').attr({ 'account-id': accountId });
        $('#btnDeleteAccount').attr({ 'account-id': accountId });
        
        $('#accountIDModal').html($('#AccountID' + accountId).html());
        $('#nameAccountModal').html($('#Name' + accountId).html());
        $('#sexAccountModal').html($('#Sex' + accountId).html());
        $('#birthdayAccountModal').html($('#Birthday' + accountId).html());
        $('#phoneAccountModal').html($('#Phone' + accountId).html());
        $('#emailAccountModal').html($('#Mail' + accountId).html());
        $('#cityAccountModal').html($('#CityName' + accountId).html());
        $('#townshipAccountModal').html($('#TownshipName' + accountId).html());

        var status = $('#accountStatus' + accountId).attr('statusID');
        if (status === '1') {
            $('#btnLockAccount').css({ 'display': 'block' });
            $('#btnUnlockAccount').css({ 'display': 'none' });
        }
        else if (status === '2') {
            $('#btnLockAccount').css({ 'display': 'none' });
            $('#btnUnlockAccount').css({ 'display': 'block' });
        }
    });

    $('.account-row-approval').click(function () {
        accountId = $(this).attr('id');
        $('#btnLockAccount').attr({ 'account-id': accountId });
        $('#btnUnlockAccount').attr({ 'account-id': accountId });
        $('#btnApprovalAccount').attr({ 'account-id': accountId });
        $('#btnDeleteAccount').attr({ 'account-id': accountId });

        $('#accountIDModal').html($('#AccountID' + accountId).html());
        $('#nameAccountModal').html($('#Name' + accountId).html());
        $('#sexAccountModal').html($('#Sex' + accountId).html());
        $('#birthdayAccountModal').html($('#Birthday' + accountId).html());
        $('#phoneAccountModal').html($('#Phone' + accountId).html());
        $('#emailAccountModal').html($('#Mail' + accountId).html());
        $('#cityAccountModal').html($('#CityName' + accountId).html());
        $('#townshipAccountModal').html($('#TownshipName' + accountId).html());
        $('#btnApprovalAccount').css({ 'display': 'block' });
        $('#btnLockAccount').css({ 'display': 'none' });
        $('#btnDissApprovalAccount').css({ 'display': 'none' });
        $('#btnUnlockAccount').css({ 'display': 'none' });
    });
    
    $("#btnUnlockAccount").click(function () {
        var accID = $("#btnUnlockAccount").attr("account-id");
        $('#unlock1').css({ 'display': 'none' });
        $('#unlock2').css({ 'display': 'block' });
        $(this).attr('disabled', 'true');
        $.ajax({
            async: true,
            type: 'POST',
            url: '/Admin/UnBlockAccount',
            dataType: 'json',
            data: { accountID: accID },
            success: function (response) {
                if (response.result == true) {
                    $('#accountStatus' + accID).attr({ 'statusid': '1' });
                    $('#accountStatus' + accID).removeClass('text-danger');
                    $('#accountStatus' + accID).addClass('text-success');
                    $('#accountStatus' + accID).html('Sẵn sàng');
                    $('#closeModalAccountInfo').trigger('click');
                    $('#unlock1').css({ 'display': 'block' });
                    $('#unlock2').css({ 'display': 'none' });
                    $("#btnUnlockAccount").removeAttr('disabled');
                    $('#contentNotification').html("Đã mở khoá tài khoản " + accID);
                    $('#notificationModal').modal('show');
                }
            }
        });
    });

    $("#btnApprovalAccount").click(function () {
        var accID = $("#btnApprovalAccount").attr("account-id");
        $('#duyet1').css({ 'display': 'none' });
        $('#duyet2').css({ 'display': 'block' });
        $(this).attr('disabled','true');
        $.ajax({
            async: true,
            type: 'POST',
            url: '/Admin/ApprovalAccount',
            dataType: 'json',
            data: { accountID: accID },
            success: function (response) {
                if (response.result == true) {
                    $('#accountStatus' + accID).attr({ 'statusid': '1' });
                    $('#accountStatus' + accID).removeClass('text-danger');
                    $('#accountStatus' + accID).addClass('text-success');
                    $('#accountStatus' + accID).html('Đã phê duyệt');
                    $('#closeModalAccountInfo').trigger('click');
                    $('#duyet1').css({ 'display': 'block' });
                    $('#duyet2').css({ 'display': 'none' });
                    $("#btnApprovalAccount").removeAttr('disabled');
                }
            }
        });
        $('#contentNotification').html("Đã khoá tài khoản " + accountID);
        $('#notificationModal').modal('show');
    });

    $("#btnLockAccount").click(function () {
        var accID = $("#btnLockAccount").attr("account-id");
        $('#block1').css({ 'display': 'none' });
        $('#block2').css({ 'display': 'block' });
        $(this).attr('disabled', 'true');
        $.ajax({
            async: true,
            type: 'POST',
            url: '/Admin/LockAccount',
            dataType: 'json',
            data: { accountID: accID },
            success: function (response) {
                if (response.result == true) {
                    $('#accountStatus' + accID).attr({ 'statusid': '2' });
                    $('#accountStatus' + accID).removeClass('text-danger');
                    $('#accountStatus' + accID).addClass('text-danger');
                    $('#accountStatus' + accID).html('Bị khoá');
                    $('#closeModalAccountInfo').trigger('click');
                    $('#block1').css({ 'display': 'block' });
                    $('#block2').css({ 'display': 'none' });
                    $("#btnLockAccount").removeAttr('disabled');
                    $('#contentNotification').html("Đã khoá tài khoản " + accID);
                    $('#notificationModal').modal('show');
                }
            }
        });
    });
});
