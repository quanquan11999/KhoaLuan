var criteriaID = 0;
var stt = 0;
var max = 0;
$(document).ready(function () {
    $('.criteria-del,.criteria-edit').click(function () {
        max = $('#max-row').html();
        criteriaID = $(this).attr('criteriaID');
        stt = $(this).attr('stt');
    });

    $('#del-confirm').click(function () {
        $('#xoa1').css({ 'display': 'none' });
        $('#xoa2').css({ 'display': 'block' });
        $(this).attr('disabled', 'true');
        setTimeout(function () {
            $.ajax({
                async: true,
                type: 'POST',
                url: '/Admin/DeleteCriteria',
                dataType: 'json',
                data: { criteriaID: criteriaID },
                success: function (response) {
                    if (response.result == true) {
                        $('#xoa1').css({ 'display': 'block' });
                        $('#xoa2').css({ 'display': 'none' });
                        var j = 1;
                        for (var i = 1; i <= max; i++) {
                            if (i != stt) {
                                $('#stt' + i).html(j);
                                $('#stt' + i).attr('id', 'stt' + j);
                                $('#del' + i).removeAttr('stt');
                                $('#del' + i).attr('stt', j);
                                $('#del' + i).attr('id', 'del' + j);
                                j++;
                            }
                        }
                        $('#stt' + stt).empty();
                        $('#' + criteriaID).remove();
                        $('#max-row').html(max - 1);
                        $("#del-confirm").removeAttr('disabled');
                        $('#modalDel').modal('hide');
                    }
                }
            });
            criteriaName = $("#criteriaName" + criteriaID).html();
            $('#contentNotification').html("Đã xoá tiêu chí: " +criteriaName);
            $('#notificationModal').modal('show');
        }, 700);
    });

    $('.criteria-edit').click(function () {
        var name = $('#criteriaName' + criteriaID).html();
        $('#criteriaName').val(name);
    });

    $('#btn-save').click(function () {
        $('#sua1').css({ 'display': 'none' });
        $('#sua2').css({ 'display': 'block' });
        $(this).attr('disabled', 'true');
        
        setTimeout(function () {
            var newName = $('#criteriaName').val();
            $('#criteriaName' + criteriaID).html(newName);
            $.ajax({
                async: true,
                type: 'POST',
                url: '/Admin/EditCriteria',
                dataType: 'json',
                data: { criteriaID: criteriaID, criteriaName: newName },
                success: function (response) {
                    if (response.result == true) {
                        $('#sua1').css({ 'display': 'block' });
                        $('#sua2').css({ 'display': 'none' });
                        $("#btn-save").removeAttr('disabled');
                        $('#myModalEdit').modal('hide');
                    }
                }
            });
            $('#contentNotification').html("Lưu thành công");
            $('#notificationModal').modal('show');
        }, 600);
    });
});
