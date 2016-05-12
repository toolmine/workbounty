

function SearchResult() {
   
                

        var id = $("#SearchItem").val();
        if (id != "") {
            var url = "/home/SearchWorkitem?searchWorkitemValue=" + encodeURIComponent(id);
            document.location.href = url;
          }
        
};
