var stockTypeTable;

$(document).ready(function () {

    stockTypeTable = $("#stockTypeTable").DataTable({

        "ajax": {
            "url": "/Employee/StockType/GetStockTypes"
        },

        "columns": [

            { "data": "name" },
            { "data": "stockUnit" },
            {
                "data": "id",
                "render": function (data) {

                    return `
                        <a href="/Employee/StockType/Edit/${data}">Edit</a>
                        <a href="/Employee/StockType/Delete/${data}">Delete</a>
                    
                    `

                }
            }



        ]





    })


})