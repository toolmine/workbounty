
$(document).ready(function () {
    $("#labelSuccessMessage").hide();
});


function add(id) {
    var Data = {
        "UserID": id,
        "TeamID": document.getElementById("Teamid").value,
        "WorkitemID": document.getElementById("Workid").value
    };
    $.ajax({
        url: "/Workitem/ViewAssignedWorkitem",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(Data),
        dataType: "json",
        success: function (response) {
            if (response == "Success")
                {
                $("#labelSuccessMessage").show();
            window.location.href = "/Home/Dashboard/";
            }
            else
            {
                alert("Error");
            }
        },

        error: function (x, e) {
            alert("Error");
        }
    });
}