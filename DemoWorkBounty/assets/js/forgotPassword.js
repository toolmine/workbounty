$(document).ready(function () {
    $("#alertMessage").hide();
    $("#passwordLinkSuccess").hide();
    $("#alertBlankInputMessage").hide();

});
$(function () {
    $("#forgotButton").click(function (e) {

        e.preventDefault();
        var id = {
            "Email": $("#emailID").val(),
        };

        if (id.Email == "") {
            $("#alertBlankInputMessage").show();
        }
        else {
            $.ajax({
                url: "/Home/ForgotPassword/",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(id),
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    if (response.success) {
                        $("#passwordLinkSuccess").hide();
                        location.href = response.redirectURL;
                    }
                    else {
                        $("#alertMessage").show();
                    }
                },

                error: function (x, e) {
                    $("#alertMessage").show();
                }
            });
        }
    });

});