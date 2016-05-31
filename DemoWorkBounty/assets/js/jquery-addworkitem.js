var radioValue;
var amount;
var filepath;

$(document).ready(function () {

    $("#alertMessage").hide();
    $("#btn").click(function () {

        var radioValue = $("input[name='formfieldradio']:checked").val();
        var amount = $("#amount").val();
        sessionStorage.setItem('key1', radioValue);
        sessionStorage.setItem('key2', amount);
        $("#myModal").fadeOut();
        var res ='Voucher:' +radioValue +', Price:$' + amount;
        $("#AmountError").text(res);

    });
});



function AddWorkitem() {

    //$('#myFile').on('change', function (e) {
    //    var files = e.target.files;
    //    //var myID = 3; //uncomment this to make sure the ajax URL works
    //    if (files.length > 0) {
    //        if (window.FormData !== undefined) {
    //            var data = new FormData();
    //            for (var x = 0; x < files.length; x++) {
    //                data.append("file" + x, files[x]);
                    
    //            }
               
    //        }
    //    }
    //});
        
    
                var d = new Date();
                var dueDate = $("#DueDate").val();
                var startDate = $("#StartDate").val();
                var startDateObject = new Date(startDate);
                var dueDateObject = new Date(dueDate);
                var fileInput = document.getElementById('myFile');
                var fileName = fileInput.value.split(/(\\|\/)/g).pop();
    
                var newitem = {};
                newitem.Title = $("#Title").val();
                newitem.Summary = $("#Summary").val();
                newitem.StartDate = startDateObject;
                newitem.DueDate = dueDateObject;
                newitem.PublishedTo = $("#TeamList").val();
                newitem.DocumentFilePath = $("#myFile").val();
                newitem.ProposedReward = sessionStorage.getItem('key1');
                newitem.Amount = sessionStorage.getItem('key2');
                newitem.CreatedBy = $("#Userid").val();
                newitem.CreatedDateTime = d;
                newitem.ModifyBy = $("#Userid").val();
                newitem.ModifyDateTime = d;
                newitem.Status = true;
                newitem.Remarks = "Good";
                newitem.IsOpenForGroup = true;
                newitem.Content = data;
  
           

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
        $.ajax({
            type: "POST",
            url: '/Home/AddWorkitem/',
            data: JSON.stringify({ addWorkitemData: newitem}),
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





$('#Summary').on('keypress', function () {
    limitText(this, 300)
});

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

