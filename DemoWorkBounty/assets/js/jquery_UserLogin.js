$(document).ready(function () {
    $("#alertMessage").hide();
    $("#loginWarningMessage").hide();

});
$(function () {
    $("#loginButton").click(function (e) {
        $("#loading").show();
        $("#login-box").fadeOut(4000);  

        e.preventDefault();
        var id = {
            "Email": $("#Email").val(),
            "Password": $("#Password").val()
        };

        if (id.Email == "" || id.Password=="")
        {
            $("#loginWarningMessage").show();
        }
        else
            {
        $.ajax({
            url: "/Home/Login/",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(id),
            dataType: "json",
            success: function (response) {
                console.log(response);
                if (response.success) {
                    location.href = response.redirectURL;
                }
                else {
                    $("#alertMessage").show();
                }
                $("#loading").hide();
            },

            error: function (x, e) {
                $("#loginWarningMessage").show();
            }
        });
        }
    });

});