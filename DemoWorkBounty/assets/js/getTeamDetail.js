$(document).ready(function () {
    $("#recent-box1").hide();
    $("#teamAlertMessage").hide();
    $("#noDateFoundMessage").hide();
    $("#alertMessage").hide();
    $("#memberAlreadyExist").hide();
    $("#addMemberFailError").hide();
    $("#memberNotFoundError").hide();
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
            console.log(response);
            if (response.success) {
                $("#getDataList").load("/Team/GetTeamMemberList", { teamName: $("#teamName").val() });
            }
            else {
                $("#addMemberFailError").show();
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
                if (Data.length==0) {
                    $("#memberNotFoundError").show();
                    $('#memberNotFoundError').delay(5000).fadeOut();
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

                $("#noDateFoundMessage").show();
                $('#noDateFoundMessage').delay(5000).fadeOut();

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
                item.remove();
              
                $("#getDataList").load("/Team/GetTeamMemberList", { teamName: $("#teamName").val() });
            }
            else
            {
                $("#memberAlreadyExist").show();
                $("#memberAlreadyExist").delay(5000).fadeOut();
            }
        },
        error: function (x, e) {
            $("#addMemberFailError").show();
            $('#addMemberFailError').delay(5000).fadeOut();
        }
    });
}
