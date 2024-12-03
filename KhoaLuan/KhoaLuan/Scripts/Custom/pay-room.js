$(document).ready(function () {
    $('.payRoom').click(function () {
        $('#roomPrice').val($('#motelRoomPrice').html());
        $('#month').html($(this).attr('id'));
        var billDetailsID = $(this).attr("bID");
        $('#billDetailsID').val(billDetailsID);
    });
    $('#check-out-confirm').click(function () {
        var motelID = $(this).attr('motelID');
        $.ajax({
            async: true,
            type: "POST",
            url: "/User/CheckOut",
            data: {
                motelID: motelID
            },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    window.location = "RoomDetails?motelID=" + motelID;
                } else {
                    alert("Còn nợ chưa thể trả phòng");
                }
            }
        });
    });

    setInterval(function () {
        var roomPrice = $('#roomPrice').val();
        var InternetPrice = $('#internetPrice').val();
        var electricityPrice = $('#electricalSign').val() * $('#unitElectricityPrice').val();
        var waterPrice = $('#numberWaterBlock').val() * $('#unitWaterPrice').val();
        $('#totalElectricityPrice').html(formatter.format(electricityPrice).substring(0));
        $('#totalWaterPrice').html(formatter.format(waterPrice).substring(0));
        $('#totalPrice').val(formatter.format(electricityPrice + waterPrice + parseInt(InternetPrice) + parseInt(roomPrice)).substring(0));
        $('#electricityPrice').val(electricityPrice);
        $('#waterPrice').val(waterPrice);
    }, 500);

    $('.payDetails').click(function () {
        var bID = $(this).attr('Bid');
        $.ajax({
            async: true,
            type: "POST",
            url: "/User/GetBillDetails",
            data: {
                billDetailsID: bID
            },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $('#roomPriceShow').val(formatter.format(response.roomPrice).substring(0));
                    $('#internetPriceShow').val(formatter.format(response.internetPrice).substring(0));
                    $('#electricityPriceShow').val(formatter.format(response.electricityPrice).substring(0));
                    $('#waterPriceShow').val(formatter.format(response.waterPrice).substring(0));
                    $('#totalPriceShow').val(formatter.format(response.totalPrice).substring(0));
                }
            }
        });
    });

    $('.RentalPeriod').click(function () {
        var billID = $('.billID').attr('billID');
        $.ajax({
            async: true,
            type: "POST",
            url: "/User/GetRecentDays",
            data: {
                billID: billID
            },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    var day1, month1, day2, month2;
                    if (response.month1 < 10) {
                        month1 = "0" + response.month1;
                    } else {
                        month1 = response.month1;
                    }
                    if (response.day1 < 10) {
                        day1 = "0" + response.day1;
                    } else {
                        day1 = response.day1;
                    }
                    if (response.month2 < 10) {
                        month2 = "0" + response.month2;
                    } else {
                        month2 = response.month2;
                    }
                    if (response.day2 < 10) {
                        day2 = "0" + response.day2;
                    } else {
                        day2 = response.day2;
                    }
                    var fromday = response.year1 + "-" + month1 + "-" + day1;
                    var today = response.year2 + "-" + month2 + "-" + day2;
                    $('#fromDayAdd').val(fromday);
                    $('#toDayAdd').val(today);
                }
            }
        });
    });
});
var formatter = new Intl.NumberFormat('vn-VN', {
    style: 'currency',
    currency: 'VND',
    minimumFractionDigits: 0
});