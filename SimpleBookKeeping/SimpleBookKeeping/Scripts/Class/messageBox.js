(function ($) {
    $.fn.msgBox = function (options) {

        var settings = $.extend({
            'first-button': 'Yes',
            'second-button': 'No'
        }, options);

        this.addClass("mPopup");
        this.append("<hr>");
        this.append("<div class='popup-buttons'>");
        this.append("<a data-role='button' class='margin10 popup-button'> " + settings["first-button"] + " </a>");
        this.append("<a data-role='button' class='margin10 popup-button'> " + settings["second-button"] + " </a>");
        this.append("</div>");

        $(".popup-button").button().button('refresh');

        var popup = this.mPopup({
            showOverlay: false
        });
        // open generated popup
        popup.mPopup('open');

    };
})(jQuery);