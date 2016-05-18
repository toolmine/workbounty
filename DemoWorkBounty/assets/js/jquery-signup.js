$(document).ready(function ()
{
    $("#alertMessage").hide();
    $("#btnSubmit").click(function (e) {
        e.preventDefault();
        var dateofBirth = $("#DateSelect").val();
        var dateofBirthDateObject = new Date(dateofBirth);

        var userSignupData =
            {
                "FirstName": $("#FirstName").val(),
                "LastName": $("#LastName").val(),
                "DateofBirth": dateofBirthDateObject,
                "Email": $("#Email").val(),
                "PhoneNumber": $("#PhoneNumber").val(),
                "Password": $("#Password").val(),
                "InterestedKeywords": $("#InterestedKeywords").val(),
                "isActive": true
            }
        
        if ($("#Password").val() != $("#ConfirmPassword").val()) {
            $("#ConfirmPasswordError").text("Both fields do not match");
        }
        else

        if ($("#Email").val() == "") {
            $("#EmailError").text("Email is Required");
        }
        else  if ($("#PhoneNumber").val() == "") {
            $("#PhoneNumberError").text("Phone Number is Required");
        }

       else if ($("#FirstName").val() == "") {
            $("#FirstNameError").text("First Name is Required");
        }

        else if ($("#LastName").val() == "") {
            $("#LastNameError").text("Last Name is Required");
        }
        else if (dateofBirth == "") {
            $("#DateofBirthError").text("Date Of Birth is Required");
        }
  
        else if ($("#Password").val() == "")
        {
            $("#PasswordError").text("Password is Required");
        }
        else if($("#InterestedKeywords").val() == "") 
        {
                $("#InterestedKeywordsError").text("Interested Keyword is Required");
         }
   
        else {

            $.ajax({
                url: "/Home/Signup/",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(userSignupData),
                dataType: "json",
                success: function (getResponseOfSignupData) {
                    console.log(getResponseOfSignupData);
                    if (getResponseOfSignupData.success) {
                        location.href = getResponseOfSignupData.redirectURL;
                    }
                    else {
                        $("#alertMessage").show();
                    }

                },

                error: function (x, e) {
                    $("#alertMessage").show();
                }
            });
        }
    })

});

function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode != 43 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }

    function isTextKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && charCode < 65
          && (charCode < 97 || charCode > 122))
            return false;
        return true;
    }

    function noDataKey(evt) {
        var charcode = (evt.which) ? evt.which : event.keyCode;
        if (charcode > 0 && charcode < 127)
            return false;
        return true;
    }

    
    $(document).ready(function () {
        $('#Password').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });
    $(document).ready(function () {
        $('#ConfirmPassword').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });
