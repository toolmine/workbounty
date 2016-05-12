$(document).ready(function () {
    $("#alertMessage").hide();
    $("#loginWarningMessage").hide();

});
$(function () {
    $("#loginButton").click(function (e) {
        
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
            },

            error: function (x, e) {
                $("#loginWarningMessage").show();
            }
        });
        }
    });

});