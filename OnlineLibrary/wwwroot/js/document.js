var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable =  $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Document/GetAll"
        },
        "columns": [
            { "data": "title", "width": "25%"},
            { "data": "isbn", "width": "15%"},
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "author.name", "width": "15%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center" >
                                <a href="/Admin/Document/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/Document/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="far fa-trash-alt"></i>
                                </a>    
                            </div>
                            `; 
                }, "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
