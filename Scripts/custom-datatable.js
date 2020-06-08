$(document).ready(function () {
    $('#employeeTable').DataTable(
    {
        "scrollX": true,
        "columnDefs": [
            { "width": "5%", "targets": [0] },
            { "width": "10%", "searchable": false, "orderable": false,  "targets": [7] },
            { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11,12,13] },
        ],
        "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
        "processing": true,
        "serverSide": true
       
    });
});

