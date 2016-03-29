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
        url: "/api/Showitem/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(item),
        dataType: "json",
        success: function (response) {

            alert(response);

        },

        error: function (x, e) {
            alert("Error");


        }
    });
}