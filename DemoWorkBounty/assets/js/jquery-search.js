function handleEnter(inField, e) {
    var charCode;
   
    if (e && e.which) {
        charCode = e.which;
    } else if (window.event) {
        e = window.event;
        charCode = e.keyCode;
    }

    if (charCode == 13) {
        var id = $("#SearchItem").val();
        if (id == null) {
            alert("Please enter Workitem Name");
        }
        else {
            //var url = "/home/SearchWorkitem?searchWorkitemValue=" + encodeURIComponent(id);
            //document.location.href = url;

           $.ajax({
                url: "/Home/SearchWorkitem",
                type: 'POST',
                data: {'searchWorkitemValue':id},
                success: function (data)
                {
                    var url = "/SearchWorkitem";
                    document.location.href = url;
                }
            });
        }
    }
}

function SearchResult() {
   
                

        var id = $("#SearchItem").val();
        if (id == null) {
            alert("Please enter Workitem Name");
        }
        else {
            var url = "/home/SearchWorkitem?searchWorkitemValue=" + encodeURIComponent(id);
            document.location.href = url;
        }

   
};
