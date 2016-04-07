function AddUserData() {

    var item = {
        "FirstName": $("#FirstName").val(),
        "LastName": $("#LastName").val(),
        "DateofBirth": $("#DateSelect").val().toLocaleString().substring(0, 10),
        "Email": $("#Email").val(),
        "PhoneNumber": $("#PhoneNumber").val(),
        "Password": $("#Password").val(),
        "InterestedKeywords": $("#InterestedKeywords").val(),
        "isActive": true
    };

    $.ajax({
        url: "/Home/Signup/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(item),
        dataType: "json",
        success: function (response) {
            if (response == "Success") {
                var url = "/home/afterlogin/"
                document.location.href = url;
            }
            else {
                alert("Signup Fail");
            }

        },

        error: function (x, e) {
            alert("Error");


        }
    });
}