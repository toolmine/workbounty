    $(document).ready(function () {
        $("#recent-box1").hide(),
        $.getJSON("/api/AddTeamMember/",
         
       function (Data) {
    
       });

    });
function add(item) {


    var id = $(item).attr("id");
    var memberData = {
        "UserID": id,
        "IsActive": true,
        "TeamName":"z",
        "TeamUserInfoID": $("#Userid").val()
    };


    $.ajax({
        url: "/Api/DisplayReward/AddMemberData/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(memberData),
        dataType: "json",
        success: function (response) {
            alert("Member Added ")
            item.remove()
        },

        error: function (x, e) {
            alert("Error");


        }
    });



}


function show() {
    $("#simple-table tr").remove();
    var id = $('#itId').val();

    $.getJSON("/api/AddTeamMember/" + id,

        function (Data) {
            $("#simple-table").append('<tr><th>Member Name</th><th>Email</th><th>Action</th></tr>');
            var arrayLength = Data.length;
            //idArray.splice(0, idArray.length)
            for (var i = 0; i < arrayLength; i++) {
                $("#simple-table").append('<tr><td>' + Data[i].FirstName + '</td><td>' + Data[i].Email + '</td><td><input type="button" id=' + Data[i].UserID + ' value="Add Member" onclick="add(this); return false;" class="btn btn-minier btn-purple" /></td></tr>');
            }


        })

    .fail(

        function (jqXHR, textStatus, err) {

            alert('Error: ' + err);

        });

}


function submit() {

    var teamData = {
        "TeamName": $("#txtTeamName").val(),
        "UserID": $("#Userid").val(),
        "IsActive": false,
        "TeamUserInfoID": $("#Userid").val()
    };


    $.ajax({
        url: "/Api/AddTeamMember/AddTeamData/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(teamData),
        dataType: "json",
        success: function (response) {
            alert("Team Added ")
            $("#recent-box1").show()
        },

        error: function (x, e) {
            alert("Error");


        }
    });


}

