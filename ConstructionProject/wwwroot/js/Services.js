var serviceTable;

$(document).ready(function () {
    serviceTable = $("#serviceTable").DataTable({

        "ajax": {
            "url": "/Admin/Service/GetAllServices"
        },
        "columns": [

            { "data": "name" },
            { "data": "description" },
            {
                "data": "isFeatured",
                "render": function (data) {
                    if (data) {
                        return "Yes"
                    }
                    else {
                        return "No"
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Service/Edit/${data}" >Edit</a>
                        <a href="/Admin/Service/Delete/${data}" >Delete</a>
                    `
                }
            }


        ]



    })


})