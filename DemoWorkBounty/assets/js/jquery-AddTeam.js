$(document).ready(function () {
    $("#recent-box1").hide();
    $("#teamAlertMessage").hide();
    $("#noDateFoundMessage").hide();
    $("#alertMessage").hide();
    $("#teamWarningMessage").hide();
    $("#memberAlreadyExist").hide();
    
});


function add(item) {
    var id = $(item).attr("id");
    var memberData = {
        "UserID": id,
        "IsActive": true,
        "TeamName": $("#txtTeamName").val(),
        "TeamUserInfoID": $("#teamid").val()
    };
    $.ajax({
        url: "/Team/AddMember",
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
                $('#memberAlreadyExist').delay(5000).fadeOut();
            }
        },
        error: function (x, e) {
            $("#noDateFoundMessage").show();
            $('#noDateFoundMessage').delay(5000).fadeOut();
         
        }
    });
}

function show() {
    $("#teamAlertMessage").hide();
    $("#simple-table tr").remove();
    var id = $('#itId').val();
    $.getJSON("/api/FindMember/" + id,


            function (Data) {
                if (Data == null) {
                    $("#noDateFoundMessage").show();
                    $('#noDateFoundMessage').delay(5000).fadeOut();
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

function submit() {
     $("#teamWarningMessage").hide();
    var teamName = $("#txtTeamName").val();

    var teamData = {
        "TeamName": $("#txtTeamName").val(),
        "UserID": $("#Userid").val(),
        "IsActive": false,
        "TeamUserInfoID": $("#Userid").val()
    };

    if (teamName == "") {
        $("#alertMessage").show();
        $('#alertMessage').delay(5000).fadeOut();
        document.getElementById("txtTeamName").value = "";
    }
    else {
        $.ajax({
            url: "/Team/Addteam",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(teamData),
            dataType: "json",
            success: function (response) {
                if (response != 0) {
                    $("#teamAlertMessage").show();
                    $("#AddTeamButton").hide();
                    $("#recent-box1").show();
                }
                else
                {
                    $("#teamWarningMessage").show();
                    $('#teamWarningMessage').delay(5000).fadeOut();
                }
            },
            error: function (x, e) {
                $("#noDateFoundMessage").show();
                $('#noDateFoundMessage').delay(5000).fadeOut();
            }
        });
    }
}
