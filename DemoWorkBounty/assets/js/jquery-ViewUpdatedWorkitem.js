$(document).ready(function () {
    $("#labelSuccessMessage").hide();
});


function ApplyReward(item,item1) {
    var feedback = document.getElementById(item1).value;
    var id = {
        "UserID": $(item).attr("id"),
        "WorkitemID": $("#Workid").val(),
       "Remarks": feedback
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