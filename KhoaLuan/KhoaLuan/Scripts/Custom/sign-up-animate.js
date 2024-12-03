$(document).ready(function(){
    $('.form-control').focus(function () {
        //if ($(this).val() !== "") {
        //    $(this).css({ "border-color": "#000" });
        //}
        //else {
        //    $(this).css({ "border-color": "#888" });
        //}
        $(this).css({ "border-color": "#888" });

		var label = $(this).closest('div').find('label');
		label.css({
			"top": "-10px", 
			"color": "#666", 
			"font-size": "12px", 
			"font-style": "italic"
		});
	});

	$('.form-control').focusout(function(){
        if ($(this).val() === "") {
            var label = $(this).closest('div').find('label');
            label.css({
                "top": "10%",
                "color": "inherit",
                "font-size": "inherit",
                "font-style": "normal"
            });
        }
        $(this).css({ "border-color": "#ddd" });

	});

	$('.show-password-icon').click(function () {
		var password = $(this).closest('div').find('input');
		var rePassword = $('#rePassword');
		if (password.attr('type') === "password") {
			password.attr('type', 'text');
			rePassword.attr('type', 'text');
			$(this).html('visibility_off');
		}
		else {
			password.attr('type', 'password');
			rePassword.attr('type', 'password');
			$(this).html('visibility');
		}
	});
});