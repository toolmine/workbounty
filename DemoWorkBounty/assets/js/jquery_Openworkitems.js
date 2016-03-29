$(document).ready(function () {

    $.ajax({
        url: "/Home/AfterLogin/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (Data) {
            alert(Data);
            $("#OpenWorkitem").append('<tr><th>Sr no</th><th>Title</th><th>First name</th><th>Summary</th><th>Reward</th><th>Amount</th><th>Action</th></tr>');
            var arrlength = Data.length;
            for (var i = 0; i < arrlength; i++) {
                $("#OpenWorkitem").append('<tr><td>' + i + '</td><td>' + Data[i].Title + '</td><td>' + Data[i].FirstName + '</td><td>' + Data[i].Summary + '</td><td>' + Data[i].ProposedReward + '</td><td>' + Data[i].Amount + '</td><td><input type="button" id=' + Data[i].WorkItemID + ' value="Apply" onclick="add(this); return false;" class="btn btn-minier btn-purple" /></td></tr>');

            }
            
        },

        error: function (x, e) {
            alert('Failed');
            alert(x.responseText);
            alert(x.status);

        }
    });



  
function add(id) {

    var id = $(id).attr("ID")

    var url = "/work/detailworkitem?id=" + encodeURIComponent(id);
    document.location.href = url;

}