$(document).ready(function () {
    $("#recent-box1").hide();
    $("#teamAlertMessage").hide();
    $("#noDateFoundMessage").hide();
    $("#alertMessage").hide();
    $("#memberAlreadyExist").hide();
});


function removeMember(item) {
    var id = $(item).attr("id");
    var teamID = $(item).data('teamuserid');    
    var updateMemberData = {
        "UserID": id,
        "IsActive": false,
        "TeamUserInfoID": teamID
    };
    $.ajax({
        url: "/Team/UpdateMember",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(updateMemberData),
        dataType: "json",
        success: function (response) {
            if (response == "Success") {
                item.remove()
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

function show() {
   
    $("#simple-table tr").remove();
    var id = $('#itId').val();
    var teamID = $('#teamID').val();
    var teamName = $('#teamName').val();
    $.getJSON("/api/FindMember/" + id,

            function (Data) {
                if (Data == null) {
                    $("#noDateFoundMessage").show();
                }
                else {
                    $("#simple-table").append('<tr><th>Member Name</th><th>Email</th><th>Action</th></tr>');
                    var arrayLength = Data.length;
                    for (var i = 0; i < arrayLength; i++) {
                        $("#simple-table").append('<tr><td>' + Data[i].FirstName + '</td><td>' + Data[i].Email + '</td><td><input type="button" id=' + Data[i].UserID + ' value="Add Member" onclick="add(this); return false;" class="btn btn-minier btn-purple" /></td></tr>');
                    }
                }

            })

        .fail(

            function (jqXHR, textStatus, err) {

                alert('Error: ' + err);

            });

}
function add(item) {
    var id = $(item).attr("id");
    var teamID = $('#teamID').val();
    var memberData = {
        "UserID": id,
        "IsActive": true,
        "TeamName": $("#Teamname").val(),
        "TeamUserInfoID": teamID
    };
    $.ajax({
        url: "/Team/UpdateNewMember",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(memberData),
        dataType: "json",
        success: function (response) {
            if (response == "Success") {
                item.remove()
            }
            else
            {
                $("#memberAlreadyExist").show();
            }
        },
        error: function (x, e) {
            alert("Error");
        }
    });
}
