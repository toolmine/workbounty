$(document).ready(function () {
    function add(id) {

        var id = $(id).attr("id")

        var url = "/work/detailworkitem?id=" + encodeURIComponent(id);
        document.location.href = url;

    }
});
