
$(document).ready(function () {
    $.getJSON("/api/AddTeamMember/",

   function (Data) {

   });

});
function add(item)
{
        
  
    //var data = {
    // id:$(item).attr("id"),
    //currentid: $("Userid").val()
    //}

    var teamData = {
        "TeamName": $("#txtTeamName").val(),
        "UserID": data,
        "IsActive": true,
        "TeamUserInfoID": $("#Userid").val()
    };


    $.ajax({
        url: "/Api/AddTeamMember/AddTeamData/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(teamData),
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

  
function show() 
{
    $("#simple-table tr").remove();
    var id = $('#itId').val();

    $.getJSON("/api/AddTeamMember/" + id,
           
        function (Data) {
            $("#simple-table").append('<tr><th>Member Name</th><th>Email</th><th>Action</th></tr>');
            var arrayLength = Data.length;
            //idArray.splice(0, idArray.length)
            for (var i = 0; i < arrayLength; i++)
            {
                $("#simple-table").append('<tr><td>' + Data[i].FirstName + '</td><td>' + Data[i].Email + '</td><td><input type="button" id=' + Data[i].UserID + ' value="Add Member" onclick="add(this); return false;" class="btn btn-minier btn-purple" /></td></tr>');
            }

         
        })

    .fail(

        function (jqXHR, textStatus, err) {

            alert('Error: ' + err);

        });

}
