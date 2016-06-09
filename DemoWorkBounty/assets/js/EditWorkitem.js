var radioValue;
var amount;
var filepath;

$(document).ready(function () {

    sessionStorage.removeItem('key1');
    sessionStorage.removeItem('key2');
    sessionStorage.removeItem('key3');
    sessionStorage.removeItem('key4');
    sessionStorage.removeItem('key5');
    sessionStorage.removeItem('key6');


    $("#alertMessage").hide();
    $("#btn").click(function () {

        var radioValue = $("input[name='formfieldradio']:checked").val();
        var amount = $("#amount").val();
        var title = $("#title").val();
        var summary = $("#Summary").val();
        var team = $("#TeamList").val();
        var workitemID = $("#Workid").val();

        sessionStorage.setItem('key1', radioValue);
        sessionStorage.setItem('key2', amount);
        sessionStorage.setItem('key3', title);
        sessionStorage.setItem('key4', summary);
        sessionStorage.setItem('key5', team);
        sessionStorage.setItem('key6', workitemID);
        $("#myModal").fadeOut();
      
        var d = new Date();

        var newitem = {};
        newitem.Title = sessionStorage.getItem('key3');
        newitem.Summary = sessionStorage.getItem('key4');
        newitem.PublishedTo = sessionStorage.getItem('key5');
        newitem.ProposedReward = sessionStorage.getItem('key1');
        newitem.Amount = sessionStorage.getItem('key2');
        newitem.ModifyBy = $("#Userid").val();
        newitem.ModifyDateTime = d;
        newitem.WorkitemID = $("#Workid").val();

        $.ajax({
            type: "POST",
            url: '/Workitem/EditWorkitem/',
            data: JSON.stringify({ addWorkitemData: newitem }),
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

function AddWorkitem() {
    var d = new Date();

    var newitem = {};
    newitem.Title = sessionStorage.getItem('key3');
    newitem.Summary = sessionStorage.getItem('key4');
    newitem.PublishedTo = sessionStorage.getItem('key5');
    newitem.ProposedReward = sessionStorage.getItem('key1');
    newitem.Amount = sessionStorage.getItem('key2');
    newitem.ModifyBy = $("#Userid").val();
    newitem.ModifyDateTime = d;
   


    if ($("#Title").val() == "") {
        $("#TitleError").text("Title is Required");
    }
    else if ($("#Summary").val() == "") {
        $("#SummaryError").text("Summary is Required");
    }

    else if ($("#StartDate").val() == "") {
        $("#StartdateError").text("Start Date is Required");
    }

    else if ($("#DueDate").val() == "") {
        $("#DuedateError").text("Due Date is Required");
    }

    else {
        var summaryVal = $("#Summary").val();
        var summaryText = summaryVal.indexOf(' ') >= 2;

        if (summaryText == false) {
            $("#SummaryError").text("Maximum character limit exists");
        }
        else {
            $.ajax({
                type: "POST",
                url: '/Workitem/EditWorkitem/',
                data: JSON.stringify({ addWorkitemData: newitem }),
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
        }



    }



}

function isTextKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && charCode < 65
      && (charCode < 97 || charCode > 122))
        return false;

    return true;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function noDataKey(evt) {
    var charcode = (evt.which) ? evt.which : event.keyCode;
    if (charcode > 0 && charcode < 127)
        return false;

    return true;
}


function noSpaceKey(evt) {
    limitText(this, 300);
    if (evt.keyCode != 32) {
        $("#SummaryError").text("Space is required");
    }
    return true;


}



function limitText(field, maxChar) {
    var ref = $(field),
        val = ref.val();
    if (val.length >= maxChar) {
        ref.val(function () {
            console.log(val.substr(0, maxChar))
            $("#SummaryError").text("Summary should not be more than 300 words");
        });
    }
}