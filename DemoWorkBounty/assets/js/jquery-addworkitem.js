var text_max = 200;

$(document).ready(function () {

    $("#alertMessage").hide();
    $("#btn").click(function ()
    {

        var radioValue = $("input[name='formfieldradio']:checked").val();
        var amount = $("#amount").val();
        sessionStorage.setItem('key1', radioValue);
        sessionStorage.setItem('key2', amount);
        $("#myModal").fadeOut();
        var res = 'Voucher:' + radioValue + ', Price:$' + amount;
        $("#AmountError").text(res);

    });


    $("#filesizeerror").text("Maximum file size is 4MB");

    $('#textarea_count').html(text_max + ' characters remaining');
    $('#Summary').keyup(function ()
    {
        var text_length = $('#Summary').val().length;
        var text_remaining = text_max - text_length;

        $('#textarea_count').html(text_remaining + ' characters remaining');

    });

});


function AddWorkitem() {

    var uploadedFileSize = 0;
    var uploadedFile = document.getElementById('myFile');
    if (uploadedFile.value !== "") {
        {
            uploadedFileSize = uploadedFile.files[0].size;
            if (uploadedFileSize > 4194304) {
                $("#filesizebig").text("File is too big!");
                return null;
            }
        }
    }

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
    newitem.DocumentFilePath = fileName;
    newitem.ProposedReward = sessionStorage.getItem('key1');
    newitem.Amount = sessionStorage.getItem('key2');
    newitem.CreatedBy = $("#Userid").val();
    newitem.CreatedDateTime = d;
    newitem.ModifyBy = $("#Userid").val();
    newitem.ModifyDateTime = d;
    newitem.Status = true;
    newitem.Remarks = "Good";
    newitem.IsOpenForGroup = true;
    newitem.Content = uploadedFileSize;

    if ($("#Title").val() === "") {
        $("#TitleError").text("Title is Required");
    }
    else if ($("#Summary").val() === "") {
        $("#SummaryError").text("Summary is Required");
           
    }

    else if ($("#StartDate").val() === "") {
        $("#StartdateError").text("Start Date is Required");
    }

    else if ($("#DueDate").val() === "") {
        $("#DuedateError").text("Due Date is Required");

    }
        else {
            $("#form").submit();
        $.ajax({
            type: "POST",
            url: '/Home/AddWorkitem/',
            data: JSON.stringify({ addWorkitemData: newitem }),
            contentType: "application/json;charset=utf-8",
            processData: true,
                    success: $("#alertMessage").hide(),
                                                                                        
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
        if (charCode !== 46 && charCode > 31
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



    function trim(el) {
        el.value = el.value.
        replace(/(^\s*)|(\s*$)/gi, "").
        replace(/[ ]{2,}/gi, " ").
        replace(/\n +/, "\n");
        return;
    }
   