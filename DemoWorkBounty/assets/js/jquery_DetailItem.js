var uri = "/Api/showitem/GetDetailitems/";
$(document).ready(function () {
    $.getJSON(uri)
         .done(function (da) {
             $.each(da, function (key, val) {
                 var Itemtitle = val.Title;
                 var row = "<tr><td></td> <td>" + val.Title + "</td><td></td> <td>" + val.Summary + "</td><td></td> <td>" + val.StartDate + "</td><td></td> <td>" + val.DueDate + "</td><td></td> <td>" + val.Published + "</td><td></td> <td>" + val.Status + "</td> <td></td></tr>";

                 $(Itemtitle)
                 $(row).appendTo($('#RewardsList'));

             });
         });
});


