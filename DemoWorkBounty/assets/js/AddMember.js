$(document).ready(function () {
   
});

function update(data)
{
    var memberData = {
        "UserID": data,
        "IsActive": false,
        "TeamUserInfoID": $("#TeamInfoID").val()
    };
    $.ajax({
        url: "/Team/UpdateMember",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(memberData),
        dataType: "json",
        success: function (response) {

            item.remove()
        },
        error: function (x, e) {
            alert("Error");
        }
    });

}