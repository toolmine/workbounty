$(document).ready(function () {
    $("#filealertMessage").hide();

});

function upload() {
    document.getElementById("ButtonSubmit").disabled = false;
    $("#filesizeerror").text("");
}

function uploadData() {

    var uploadedFileSize = 0;
    var uploadedFile = document.getElementById('myFile');
    if (uploadedFile.value != "") {
        {
            uploadedFileSize = uploadedFile.files[0].size;
            if (uploadedFileSize > 4194304) {
                $("#filesizeerror").text("File is too big!");
                document.getElementById("ButtonSubmit").disabled = true;
                          };
        }


    }





}