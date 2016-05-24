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
 
}(jQuery));