

var taskListTable;

$(document).ready(function () {

    taskListTable = $("#taskListTable").DataTable({

        "ajax": {
            "url" : "/Employee/TaskList/GetAllTask"
        },

        "columns": [

            { "data": "title" },
            { "data": "project.name" },
            { "data": "description" },
            { "data": "status" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Employee/TaskList/Edit/${data}">Edit</a>
                        <a href="/Employee/TaskList/Delete/${data}">Delete</a>
                    `
                }
            }



        ]






    })




})