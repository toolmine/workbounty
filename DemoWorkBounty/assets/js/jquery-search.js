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
