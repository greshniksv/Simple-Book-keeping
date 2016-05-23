(function ($) {

    $("input[type=submit]").click(function () {
        console.log("Click");
        var login = $(".login input").val();
        var password = $(".password input").val();
        $.getJSON("/login/authorize?login=" + login + "&password=" + password + " ", function (data) {
            if (data.Result !== 0) {
                $("#dialog").html(data.Message);
                $.mobile.changePage('#dialog', 'pop', true, true);
            }
        });
    });

}(jQuery));