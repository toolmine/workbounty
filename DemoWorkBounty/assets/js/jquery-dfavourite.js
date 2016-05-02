function AddFavourites(id) {
    var item = {
        "WorkitemID": id,
        "UserID": $("#UserId").val(),
        "IsExclusive": 0,
        "IsFavourite": true,
        "IsRegistered": false
    };

    $.ajax({
        type: "POST",
        url: "/workitem/ApplyWorkitem/",
        data: JSON.stringify(item),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        processData: true,
        success: function (data, status, xhr) {
            $("#BookmarkDisplay").show();
            window.location.href = 'Dashboard';
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}

function RemoveFavourite(id) {

    var item = {
        "WorkitemID": id,
        "IsFavourite": true
    };

    $.ajax({
        type: "POST",
        url: "/workitem/RemoveFavourite/",
        data: JSON.stringify(item),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        processData: true,
        success: function (data, status, xhr) {
            $("#BookmarkDisplay").hide();
            window.location.href = 'Dashboard';
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}