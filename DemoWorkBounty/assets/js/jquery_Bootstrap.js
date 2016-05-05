$(document).ready(function () {
    var dateToday = new Date();
    $('.datepicker').datepicker({
        format: 'mm-dd-yyyy',
        startDate: '-120y',
        endDate: '-18y',
    }).on('changeDate', function (e) {
        $(this).datepicker('hide');
    });

    $("#DueDate").prop('enabled', true);

    $("#StartDate").datepicker({
        startDate: new Date(),
        format: 'mm-dd-yyyy',
       
    }).on('changeDate', function (e) {
        $(this).datepicker('hide');
        startDate = new Date(e.date.valueOf());
        startDate.setDate(startDate.getDate(new Date(e.date.valueOf())));
        if(startDate!=null)
        {
            $("#DueDate").prop('disabled', false);
            $("#DueDate").datepicker('setStartDate', startDate);
        }
       
    });

    var startDate = $("#StartDate").val();
    
    $("#DueDate").datepicker({
        format: 'mm-dd-yyyy',

    }).on('changeDate', function (e) {

        $(this).datepicker('hide');
    });



});