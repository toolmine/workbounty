function AddFavourites_v(id) {

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
            window.location.href = 'Viewitemsinterestedin';
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}

function RemoveFavourite_v(id) {

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
        window.location.href = 'Viewitemsinterestedin';
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}