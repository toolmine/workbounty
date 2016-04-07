function LoginData() {

    var id = {

        "Email": $("#Email").val(),
        "Password": $("#Password").val()
    };



    $.ajax({
        url: "/Home/Login/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(id),
        dataType: "json",
        success: function (response) {

            if (response == "Success") {
                var url = "/home/afterlogin/"
                document.location.href = url;
            }
            else
            {
                alert("Please check Email And Password")
            }
        },

        error: function (x, e) {
            alert('Failed');
            alert(x.responseText);
            alert(x.status);

        }
    });
}