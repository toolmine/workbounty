var radioValue;
var amount;
var filepath;

$(document).ready(function () {

    $("#alertMessage").hide();
    $("#btn").click(function () {

        if ($("#teamname").val() == "") {
            $("#TitleError").text("Team name is Required");
        }

        var teamname = $("#teamname").val();
        var TeamUserInfoID = $("#oldteamname").val();

            
        sessionStorage.setItem('key1', teamname);
        sessionStorage.setItem('key2', TeamUserInfoID);
        $("#myModal").fadeOut();

            
        var newitem = {};
        newitem.teamname = sessionStorage.getItem('key1');
        newitem.TeamUserInfoID = sessionStorage.getItem('key2');
            
        $.ajax({
            type: "POST",
            url: '/team/UpdateTeamName/',
            data: JSON.stringify({ teamName: newitem }),
            contentType: "application/json;charset=utf-8",
            processData: true,
            success: function (response) {
                console.log(response);
                if (response.IsSuccess) {

                    location.href = response.redirectURL;
                }
                else {
                    $("#alertMessage").show();
                }
            },
            error: function (xhr) {
                $("#alertMessage").show();
            }
        });
    });




});

    
    
    

