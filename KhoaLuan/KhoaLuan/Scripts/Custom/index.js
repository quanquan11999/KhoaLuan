$(document).ready(function () {
    var university = "";
    //LoadDistrict();
    //LoadDistrict1();
    // Display value of slider
    $("#priceSlider").mousemove(function (e) {
        var price = $("#priceSlider").val();
        var format = formatter.format(price);
        $("#notiprice").html(format);
    });
    $("#areaSlider").mousemove(function (e) {
        var area = $("#areaSlider").val();
        $("#notiarea").html(area);
    });
    $("#priceSlider").change(function (e) {
        var price = $("#priceSlider").val();
        var format = formatter.format(price);
        $("#notiprice").html(format);
    });

    $("#areaSlider").change(function (e) {
        var area = $("#areaSlider").val();
        $("#notiarea").html(area);
    });

    $("#province").click(LoadDistrict);
    $('#province').bind('keyup', function () { LoadDistrict(); });

    function LoadDistrict() {
        var cID = $("#province").val();
        if (cID !== null && cID != 0) {
            $.ajax({
                type: "POST",
                url: "/Address/LoadDistrict",
                data: { provinceID: cID },
                dataType: "json",
                success: function (response) {
                    //console.log(response.result);
                    $("#district").html(response.result);
                }
            });
        }
    }
    $("#city1").click(LoadDistrict1);
    $('#city1').bind('keyup', function () { LoadDistrict1(); });

    function LoadDistrict1() {
        var cID = $("#city1").val();
        if (cID !== null && cID != 0) {
            $.ajax({
                type: "POST",
                url: "/Address/LoadDistrict",
                data: { provinceID: cID },
                dataType: "json",
                success: function (response) {
                    //console.log(response.result);
                    $("#district1").html(response.result);
                }
            });
        }
    }

    $(".user-type").click(function () {
        if ($(this).val() === $('#studentType').val()) {
            $('#sectionContentStudent').slideDown();
        } else {
            $('#sectionContentStudent').slideUp();
        }
    });

    $('.close').click(function () {
        $('.ask-modal').fadeOut();
    });
});

$(document).ready(function () {
    var Open = false;
    var totalCriteria = $('#totalCriteria').html();
    $("#optionAdvance").click(function () {
        if (Open==false) {
            $("#priceSlider").prop("disabled", false);
            $("#areaSlider").prop("disabled", false);
            for (var i = 1; i <= totalCriteria; i++) {
                $("#criteria" + i).prop("disabled", false);
            }
            $("#isSearchAdvance").prop("disabled", false);
            Open = true;
        } else if (Open==true) {
            $("#priceSlider").prop("disabled", true);
            $("#areaSlider").prop("disabled", true);
            for (var i = 1; i <= totalCriteria; i++) {
                $("#criteria" + i).prop("disabled", true);
            }
            $("#isSearchAdvance").prop("disabled", true);
            Open = false;
        }
    });
});

var formatter = new Intl.NumberFormat('vn-VN', {
    style: 'currency',
    currency: 'VND',
    minimumFractionDigits: 0
});