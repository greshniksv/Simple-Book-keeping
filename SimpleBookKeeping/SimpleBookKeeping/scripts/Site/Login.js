(function ($) {

    $(document).ready(function () {
        var errorMessage = $(".result p").html();
        if (errorMessage.length > 0) {
            window.setTimeout(function () { animateWarning(); }, 1000);
        }

    });

    function animateWarning() {
        $(".result").fadeIn(1000, function () {
            window.setTimeout(function () { $(".result").fadeOut(1000); }, 5000);
        });
    }


    //$("input[type=submit]").click(function () {
    //    console.log("Click");
    //    var login = $(".login input").val();
    //    var password = $(".password input").val();
    //    $.getJSON("/login/authorize?Login=" + login + "&Password=" + password + " ", function (data) {
    //        if (data.Result !== 0) {
    //            $(".result").html(data.Message);
    //            $(".result").fadeIn(1000, function() {
    //                setTimeout(function() { 
    //                    $(".result").fadeOut(1000);
    //                }, 1000);

    //            });
    //        }
    //    });
    //});

    //$(document).ready(function () {
       
    //});
 
}(jQuery));