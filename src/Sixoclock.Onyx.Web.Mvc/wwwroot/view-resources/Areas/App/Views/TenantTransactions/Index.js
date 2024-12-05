(function () {

    var _$transactionsTable = $('#TransactionsTable');
    var _transactionService = abp.services.app.transaction;
    
    var dataTable = _$transactionsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _transactionService.getTenantTransactions
        },
        columnDefs: [
            {
                data: null,
                orderable: false,
                autoWidth: false,
                defaultContent: ''
            },
            {
                targets: 0,
                data: "transactionStartTime"
            },
            {
                targets: 1,
                data: "duration"
            },
            {
                targets: 2,
                data: "kwh"
            },
            {
                targets: 3,
                data: "tag"
            },
            {
                targets: 4,
                data: "installName"
            },
            {
                targets: 5,
                data: "groupName"
            },
            {
                targets: 6,
                data: "modelName"
            },
            {
                targets: 7,
                data: "cost"
            }
        ]
    });
    
    //function getTransactions(reload) {

    //    dataTable.ajax.reload();
    //}
    
})();