$(document).ready(function () {
    $("#labelSuccessMessage").hide();
});




function ApplyReward(item) {
    var feedbackValue= $(this).find('select.feedback').val();
    var id = {
       "ButtonID":$(item).attr("id"),
        "UserID": $("#userID").val(),
        "WorkitemID": $("#Workid").val(),
        "FeedbackID": $("#tableID").val(),
        "Remark":$("")
    };

    $.ajax({
        url: "/workitem/PayReward/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(id),
        dataType: "json",
        success: function (response) {
            if (response == "Success") {
                $("#labelSuccessMessage").show();
                window.location.href = "/Home/Dashboard/";
            }
        },

        error: function (x, e) {
            alert("Error");


        }
    });

}