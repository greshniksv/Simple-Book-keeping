(function ($) {
    $.fn.msgBox = function (options) {

        var settings = $.extend({
            'caption': 'Caption',
            'head': 'Head',
            'body': 'Body text',
            'firstButtonCaption': 'Нет',
            'secondButtonCaption': 'Да',
            'onFirstButton': null,
            'onSecondButton': null
        }, options);

        var data = "<div class=\"ui-popup-container pop in ui-popup-active\" id=\"popupDialog-popup\" style=\"max-width: 290px; top: 241.5px; left: 15px;\">\r\n" +
            "                <div data-role=\"popup\" id=\"popupDialog\" data-overlay-theme=\"a\" data-theme=\"a\" style=\"max-width:400px;\" class=\"ui-corner-all ui-popup ui-body-a ui-overlay-shadow\">\r\n" +
            "    <div data-role=\"header\" data-theme=\"a\" class=\"ui-corner-top ui-header ui-bar-a\" role=\"banner\">\r\n" +
            "        <h1 class=\"ui-title\" role=\"heading\" aria-level=\"1\">" + settings["caption"] + "<\/h1>\r\n" +
            "    <\/div>\r\n" +
            "    <div role=\"main\" class=\"ui-corner-bottom ui-content\">\r\n" +
            "        <h3 class=\"ui-title\">" + settings["head"] + "<\/h3>\r\n" +
            "        <p>" + settings["body"] + "<\/p>\r\n" +
            "        <a href=\"#\" id=\"firstButton\" data-role=\"button\" data-inline=\"true\" data-rel=\"back\" data-theme=\"a\"" +
            " role=\"button\" class=\"ui-link ui-btn ui-btn-a ui-btn-inline ui-shadow ui-corner-all\">" + settings["firstButtonCaption"] + "<\/a>\r\n" +
            "        <a href=\"#\" id=\"secondButton\" data-role=\"button\" data-inline=\"true\" data-rel=\"back\" data-transition=\"flow\"" +
            " data-theme=\"b\" role=\"button\" class=\"ui-link ui-btn ui-btn-b ui-btn-inline ui-shadow ui-corner-all\">" + settings["secondButtonCaption"] + "<\/a>\r\n" +
            "    <\/div>\r\n" +
            "<\/div><\/div>";

        //$("#popupDialog").popup("destroy");
        $("#popupDialog").remove();
        $("body").append(data);

        $("#popupDialog").popup();
        $("#popupDialog").popup("open", {
            transition: "pop",
            positionTo: "window"
        });

        $("#firstButton").click(function () {
            if (settings["onFirstButton"] !== undefined && settings["onFirstButton"] != null) {
                settings["onFirstButton"]();
            }
        });

        $("#secondButton").click(function () {
            if (settings["onSecondButton"] !== undefined && settings["onSecondButton"] != null) {
                settings["onSecondButton"]();
            }
        });

        $("#popupDialog").on("popupafterclose", function (event, ui) {
            //$("#popupDialog").popup("destroy");
        });
    };
})(jQuery);