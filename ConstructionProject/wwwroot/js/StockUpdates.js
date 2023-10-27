var stockUpdateTable;


$(document).ready(function () {

    stockUpdateTable = $("#stockUpdateTable").DataTable({

        "ajax": {
            "url": "/Employee/StockUpdate/GetStockUpdates"
        },

        "columns": [

            { "data": "project.name" },
            { "data": "date" },
            { "data": "stockType" },
            { "data": "old" },
            { "data": "new" },
            { "data": "used" },
            { "data": "remaining" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Employee/StockUpdate/Edit/${data}">Edit</a>
                        <a href="/Employee/StockUpdate/Delete/${data}">Delete</a>
                    `
                }
            }



        ]





    })





})