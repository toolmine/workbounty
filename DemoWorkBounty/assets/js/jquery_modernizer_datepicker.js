yepnope({ /* included with Modernizr */
    test: Modernizr.inputtypes.date,
    nope: {
        'css': '/assets/css/jquery-ui.custom.min.css',
        'js': '/Content/themes/base/minified/jquery.ui.datepicker.min.css'
    },
    callback: { // executed once files are loaded
        'js': function () { $('input[type=date]').datepicker({ dateFormat: "yy-mm-dd" }); } // default HTML5 format
    }
});