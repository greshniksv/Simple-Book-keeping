
var saveFunction;

function panel() {
    $("#mypanel").panel("open");
}

function showSaveButton() {
    $("#save").fadeIn(1000);
}

function saveDone() {
    showLoading();

    if (typeof saveFunction != "undefined") {
        saveFunction();
    }

    $("#save").fadeOut(1000);
}

function showLoading() {
    $(".wait-overlay").fadeIn(1000);
}

function hideLoading() {
    $(".wait-overlay").fadeOut(1000);
}

function redirect(url) {
    $("#mypanel").panel("close");
    showLoading();

    window.setTimeout(function () {
        window.location = url;
    }, 1500);
}