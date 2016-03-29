
$(document).ready(function () {
    $.getJSON("/api/showitem/",
         function (Data) {
             $("#OpenWorkitem").append('<tr><th>Sr no</th><th>Title</th><th>First name</th><th>Summary</th><th>Reward</th><th>Amount</th><th>Action</th></tr>');
             var arrlength = Data.length;
             for (var i = 0; i < arrlength; i++)
             {
                 $("#OpenWorkitem").append('<tr><td>' + i + '</td><td>' + Data[i].Title + '</td><td>' + Data[i].FirstName + '</td><td>' + Data[i].Summary + '</td><td>' + Data[i].ProposedReward + '</td><td>' + Data[i].Amount + '</td><td><input type="button" id=' + Data[i].WorkitemID + ' value="Apply" onclick="add(this); return false;" class="btn btn-minier btn-purple" /></td></tr>');
                 
              }

         });
   

});

function add(item) {

    var id = $(item).attr("id")

    $.ajax({
        url: "/api/showitem/GetAllitems/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(id),
        dataType: "json",
        success: function (response) {
            alert("Success")

        },

        error: function (x, e) {
            alert("Error");


        }
    });



}