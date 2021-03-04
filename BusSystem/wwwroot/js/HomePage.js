$(document).ready(function () {
    $('.selectSearch').select2();
});
$("form").submit(function (e) {
    var isValid = true;

    if ($("[name=fromId]").val() == $("[name=toId]").val()) {
        $("#stationError").text("Drop-off station can't be the same as pick-up station");
        isValid = false;
    } else {

        $("#stationError").text("");
    }

    var d = new Date();
    d.setHours(0, 0, 0, 0);
    if (Date.parse($("[name=departureDate]").val()) < d) {
        $("#DeptDateError").text("Invalid Departure Date");
        isValid = false;
    } else {
        $("#DeptDateError").text("");
    }
    return isValid;

});