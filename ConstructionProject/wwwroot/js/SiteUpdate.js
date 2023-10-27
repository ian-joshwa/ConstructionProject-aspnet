

var siteUpdateTable;


$(document).ready(function () {

    siteUpdateTable = $("#siteUpdateTable").DataTable({

        "ajax": {
            "url": "/Employee/SiteUpdate/GetSiteUpdates"
        },
        "columns": [
            { "data": "project.name" },
            { "data": "applicationUser.name" },
            { "data": "date" },
            { "data": "numberOfMasons" },
            { "data": "numberOfLabours" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Employee/SiteUpdate/Edit/${data}">Edit</a>
                        <a href="/Employee/SiteUpdate/Delete/${data}">Delete</a>
                    `
                }
            }


        ]



    })




})