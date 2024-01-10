// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $("#startDatePicker").datepicker({
        dateFormat: "dd.mm.yy",
        minDate: new Date(Date.now()),
        onClose: function (inputText, source) {
            
        let minDate = new Date(source.currentYear, source.currentMonth, source.currentDay);
        minDate.setDate(minDate.getDate() + 1);

        $("#endDatePicker").datepicker("option", "minDate", minDate);
            
        }
    });
    
    $("#endDatePicker").datepicker({
        dateFormat: "dd.mm.yy",
        minDate: new Date(Date.now())
    });

  });
