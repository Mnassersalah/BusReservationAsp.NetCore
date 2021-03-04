var selectCount = 0;
var message;
var Passenger;


    function OpenMsg(msg) {
        message = "Make sure that you Selected " + msg + " seats";
        Passenger = msg;
    }

$(".imgTckt").click(function () {
    console.log(selectCount);
    if ($(this).siblings("input").attr("disabled") != "disabled") {
        if ($(this).siblings("input").attr("checked") == "checked") {
            $(this).removeClass("selectedimg");
            $(this).siblings("input").removeAttr("checked")
            selectCount--;
        } else if (Passenger > selectCount) {
            $(this).siblings("input").attr("checked", "checked")
            $(this).addClass("selectedimg");
            selectCount++;
        }

        if (Passenger == selectCount) {
            $("#submitbtn").removeClass("btn btn-secondary btn-lg").addClass("btn btn-primary").prop('disabled', false);
            $("#message").css("display", "none")

        }

        if (Passenger > selectCount) {
            $("#submitbtn").removeClass("btn btn-primary").addClass("btn btn-secondary btn-lg").prop('disabled', true);
            $("#message").css("display", "inline-block")

        }


    }



});