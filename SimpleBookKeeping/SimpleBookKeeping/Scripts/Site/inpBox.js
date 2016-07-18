(function ($) {
    $.fn.inpBox = function (options) {

        var settings = $.extend({
            'password': false,
            'caption': 'Caption',
            'head': 'Head',
            'placeholder': 'Input some text',
            'firstButtonCaption': 'Нет',
            'secondButtonCaption': 'Да',
            'onFirstButton': null,
            'onSecondButton': null
        }, options);

        var inputType = "type=" + (settings["password"] === true ? "'password'" : "'text'");

        var data = "<div class=\"ui-popup-container pop in ui-popup-active\" id=\"popupDialog-popup\" style=\"max-width: 290px; top: 241.5px; left: 15px;\">\r\n" +
            "                <div data-role=\"popup\" id=\"popupDialogInpBox\" data-overlay-theme=\"a\" data-theme=\"a\" style=\"max-width:400px;\" class=\"ui-corner-all ui-popup ui-body-a ui-overlay-shadow\">\r\n" +
            "    <div data-role=\"header\" data-theme=\"a\" class=\"ui-corner-top ui-header ui-bar-a\" role=\"banner\">\r\n" +
            "        <h1 class=\"ui-title\" role=\"heading\" aria-level=\"1\">" + settings["caption"] + "<\/h1>\r\n" +
            "    <\/div>\r\n" +
            "    <div role=\"main\" class=\"ui-corner-bottom ui-content\">\r\n" +
            "        <h3 class=\"ui-title\">" + settings["head"] + "<\/h3>\r\n" +
            "        <div id='popupDialogInputTextDiv' class='ui-input-text ui-body-inherit ui-corner-all ui-shadow-inset'><input id='popupDialogInputText' " + inputType + " placeholder='" + settings["placeholder"] + "' autocomplete='off'></div> \r\n" +
            "        <a href=\"#\" id=\"firstButtonInpBox\" data-role=\"button\" data-inline=\"true\" data-rel=\"back\" data-theme=\"a\"" +
            " role=\"button\" class=\"ui-link ui-btn ui-btn-a ui-btn-inline ui-shadow ui-corner-all\">" + settings["firstButtonCaption"] + "<\/a>\r\n" +
            "        <a href=\"#\" id=\"secondButtonInpBox\" data-role=\"button\" data-inline=\"true\" data-rel=\"back\" data-transition=\"flow\"" +
            " data-theme=\"b\" role=\"button\" class=\"ui-link ui-btn ui-btn-b ui-btn-inline ui-shadow ui-corner-all\">" + settings["secondButtonCaption"] + "<\/a>\r\n" +
            "    <\/div>\r\n" +
            "<\/div><\/div>";

        //$("#popupDialogInpBox").popup("destroy");
        $("#popupDialogInpBox").remove();
        $("body").append(data);

        $("#popupDialogInpBox").popup();
        $("#popupDialogInpBox").popup("open", {
            transition: "pop",
            positionTo: "window"
        });

        $("#firstButtonInpBox").click(function () {
            var inputText = $("#popupDialogInputText").val();
            if (settings["onFirstButton"] !== undefined && settings["onFirstButton"] != null) {
                settings["onFirstButton"](inputText);
            }
        });

        $("#secondButtonInpBox").click(function () {
            var inputText = $("#popupDialogInputText").val();
            if (settings["onSecondButton"] !== undefined && settings["onSecondButton"] != null) {
                settings["onSecondButton"](inputText);
            }
        });

        $("#popupDialogInpBox").on("popupafterclose", function (event, ui) {
            //$("#popupDialogInpBox").popup("destroy");
        });
    };
})(jQuery);