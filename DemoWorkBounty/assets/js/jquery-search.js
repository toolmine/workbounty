function ShowData()
{
    var data = {
        "FirstName": $("#FirstName").val(),

    };
    $.ajax({
        url: "http://localhost:56502/Api/MyTeam",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(item),
        dataType: "json",
        success: function (response) {

            alert('Call');
        },

        error: function (x, e) {
            alert("Error");


        }
    });


}