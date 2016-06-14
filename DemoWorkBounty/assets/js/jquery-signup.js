$(document).ready(function () {
$("#alertMessage").hide();
$('#Password').bind("copy-paste not allowed", function (e) {
    e.preventDefault();
});

$('#ConfirmPassword').bind("copy-paste not allowed", function (e) {
    e.preventDefault();
});

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

        var id = document.getElementById('Email');
        var re = /(\w+)\@(\w+)\.[a-zA-Z]/g;

        if (!re.test(id.value)) {
            $("#EmailError").text("Enter valid Email address");
          }
            else
            if ($("#Password").val() != $("#ConfirmPassword").val()) {
                $("#ConfirmPasswordError").text("Both fields do not match");
            }
            else

                if ($("#Email").val() == "") {
                    $("#EmailError").text("Email is Required");
                }
                else if ($("#PhoneNumber").val() == "") {
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

                else if ($("#Password").val() == "") {
                    $("#PasswordError").text("Password is Required");
                }
                else if ($("#InterestedKeywords").val() == "") {
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

function isNumberKey(e) {
    if (e.which != 8 && e.which != 0 && e.which != 43 && (e.which < 48 || e.which > 57)) {
        $("#PhoneNumberError").html("Input should be digits only").show().fadeOut(2500);
        return false;
    }
}

function isTextKey(e) {
    if (e.which > 31 && e.which < 65 && (e.which < 97 || e.which > 122)) {
        $("#FirstNameError").html("Input should be alphabets only").show().fadeOut(2500);
        return false;
    }
}

function isTextKey1(e) {
    if (e.which > 31 && e.which < 65 && (e.which < 97 || e.which > 122)) {
        $("#LastNameError").html("Input should be alphabets only").show().fadeOut(2500);
        return false;
    }
}

function noDataKey(evt) {
    var charcode = (evt.which) ? evt.which : event.keyCode;
    if (charcode > 0 && charcode < 127)
        return false;
    return true;
}

function AvoidSpace(event) {
    var k = event ? event.which : window.event.keyCode;
    if (k == 32) return false;
}

function trim(el) {
    el.value = el.value.
    replace(/(^\s*)|(\s*$)/gi, "").
    replace(/[ ]{2,}/gi, " ").
    replace(/\n +/, "\n");
    return;
}

