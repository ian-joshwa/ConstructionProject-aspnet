var projectTable;

$(document).ready(function () {
    projectTable = $("#projectTable").DataTable({

        "ajax": {
            "url": "/Admin/Project/GetAllProjects"
        },
        "columns": [
            { "data": "name" },
            { "data": "address" },
            {
                "data": "isPublic",
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
                        <a href="/Admin/Project/Edit/${data}">Edit</a>
                        <a href="/Admin/Project/Delete/${data}">Delete</a>
                    `
                }
            }


        ]



    })




})