var postId = 0;
var accountID = "";
$(document).ready(function () {
    $('.post-row').click(function () {
        $('#criteriaModal').html("Không có tiện nghi");
        postId = $(this).attr('id');
        accountID = $(this).attr("accID");
        var motelID = $('#motelID' + postId).html();
        $('#btnApproval').attr({ 'post-id': postId });
        $('#btnDissApproval').attr({ 'post-id': postId });
        $('#btnDelete').attr({ 'post-id': postId });

        $('#titleModal').html($('#title' + postId).html());
        $('#postDayModal').html($('#postDay' + postId).html());
        $('#addressModal').html($('#address' + postId).html());
        $('#typeModal').html($('#type' + postId).html());
        $('#acreageModal').html($('#acreage' + postId).html() + ' m<sup>2</sup>');
        $('#priceModal').html(formatter.format($('#price' + postId).html()).substring(0) + ' (VND)');
        $('#maxPeopleModal').html($('#maxPeople' + postId).html() + ' người');
        $('#nameModal').html($('#name' + postId).html());
        $('#descriptionModal').html($('#description' + postId).html());
        $('#phoneModal').html($('#phone' + postId).html());
        $('#emailModal').html($('#email' + postId).html());
        $.ajax({
            async: true,
            type: 'POST',
            url: '/Admin/GetCriteria',
            dataType: 'json',
            data: { motelID: motelID },
            success: function (response) {
                if (response.success == true) {
                    $('#criteriaModal').html(response.result);
                } else {
                    $('#criteriaModal').html("Không có tiện nghi");
                }
            }
        });

        var status = $('#status' + postId).attr('status');
        if (status === '1') {
            $('#btnApproval').css({ 'display': 'none' });
            $('#btnDissApproval').css({ 'display': 'block' });
        }
        else {
            $('#btnApproval').css({ 'display': 'block' });
            $('#btnDissApproval').css({ 'display': 'none' });
        }

        var imageList = $('#image' + postId).text().split(';');
        $('.carousel-indicators').empty();
        $('.carousel-inner').empty();
        for (var i = 0; i <= imageList.length - 2; i++) {
            var carouselIndi = '';

            var carouselItem = document.createElement('div');

            var image = document.createElement('img');
            $(image).addClass('w-100');
            var url = '../Content/Images/Houserent/' + imageList[i];
            $(image).attr({ 'src': url.trim() });
            $(carouselItem).addClass('carousel-item');
            $(carouselItem).append(image);

            if (i === 0) {
                carouselItem.classList.add('active');
                carouselIndi = '<li data-target="#carouselId" data-slide-to="' + i + '" class="active"></li>';
            }
            else {
                carouselIndi = '<li data-target="#carouselId" data-slide-to="' + i + '"></li>';
            }
            $('.carousel-indicators').append(carouselIndi);
            $('.carousel-inner').append(carouselItem);
        }
    });
});

$('#btnApproval').click(function () {
    //var  prompt('Phê duyệt bài viết ?');
    var postId = $('#btnApproval').attr('post-id');
    $.ajax({
        async: true,
        type: 'POST',
        url: '/Admin/ApprovalPost',
        dataType: 'json',
        data: { postId: postId },
        success: function (response) {
            if (response.result == true) {
                $('#status' + postId).attr({ 'status': '1' });
                $('#status' + postId).removeClass('text-danger');
                $('#status' + postId).addClass('text-success');
                $('#status' + postId).html('Đã phê duyệt');
                $('#closeModalPostInfo').trigger('click');
                $('#contentNotification').html('Đã phê duyệt bài viết: "' + $('#title' + postId).html() + '"');
                $('#notificationModal').modal('show');
            }
        }
    });
    $('#duyetModal').modal('show');
});

$('#btnDissApproval').click(function () {
    $.ajax({
        async: true,
        type: 'POST',
        url: '/Admin/DissApprovalPost',
        dataType: 'json',
        data: { postId: postId },
        success: function (response) {
            if (response.result == true) {
                $('#status' + postId).attr({ 'status': '0' });
                $('#status' + postId).removeClass('text-success');
                $('#status' + postId).addClass('text-danger');
                $('#status' + postId).html('Chưa phê duyệt');
                $('#closeModalPostInfo').trigger('click');
                $('#contentNotification').html('Đã bỏ phê duyệt bài viết: "' + $('#title' + postId).html() + '"');
                $('#notificationModal').modal('show');
            }
        }
    });
    $('#boDuyetModal').modal('show');
});

$('#btnDeletePost').click(function () {
    $('#modalDelPostConfirm').modal('show');
});

$('#del-post-confirm').click(function () {
    $.ajax({
        async: true,
        type: 'POST',
        url: '/User/DelPost',
        dataType: 'json',
        data: {
            postId: postId
        },
        success: function (response) {
            if (response.success == true) {
                window.location = "AllPost";
            }
        }
    });
});

var formatter = new Intl.NumberFormat('vn-VN', {
    style: 'currency',
    currency: 'VND',
    minimumFractionDigits: 0
});