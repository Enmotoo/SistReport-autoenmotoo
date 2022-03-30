$(document).ajaxStart(function () {
    $("#backLoader").show();
}).ajaxStop(function () {
    $("#backLoader").hide();
});