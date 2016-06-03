$(document).ready(function () {
    $("#warningMessage").hide();

});

function upload() {
    document.getElementById("ButtonSubmit").disabled = false;
    $("#warningMessage").hide();
}

function uploadData() {

    var uploadedFileSize = 0;
    var uploadedFile = document.getElementById('myFile');
    if (uploadedFile.value != "") {
        {
            uploadedFileSize = uploadedFile.files[0].size;
            if (uploadedFileSize > 4194304) {
                $("#warningMessage").show();
                document.getElementById("ButtonSubmit").disabled = true;
                          };
        }


    }





}