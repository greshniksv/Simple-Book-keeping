
var saveFunc;
var backUrl;

function panel() {
    $("#mypanel").panel("open");
}

function showSaveButton() {
    $("#save").fadeIn(1000);
}

function doBack() {
    if (typeof backUrl != "undefined") {
        redirect(backUrl);
    }
}

function doSave() {
    
    if (typeof saveFunc != "undefined") {
        showLoading();
        $("#save").fadeOut(500, function () {
            saveFunc();
        });
    } else {
        $("#save").fadeOut(100);
        alert("Nothing to save");
    }
}

function showLoading() {
    $(".wait-overlay").fadeIn(500);
}

function hideLoading() {
    $(".wait-overlay").fadeOut(500);
}

function redirect(url) {
    $("#mypanel").panel("close");
    showLoading();

    window.setTimeout(function () {
        window.location = url;
    }, 500);
}

function logOut() {
    window.location.href = "/Login/LogOut";
}