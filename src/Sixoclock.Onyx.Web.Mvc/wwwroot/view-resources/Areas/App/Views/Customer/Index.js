(function () {

    var _$customersTable = $('#CustomersTable');
    var _$filterForm = $('#CustomerFilterForm');
    var _customerService = abp.services.app.customer;
    var _createCustomerModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Customer/CreateCustomerModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customer/_CreateCustomerModal.js',
        modalClass: 'CreateCustomerModal'
    });

    var _customerServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Customer/CustomerServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customer/_CustomerServices.js',
        modalClass: 'CustomerServicesModel'
    });

    var _dashboardModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Customer/CustomerDashboardModal'
    });
    $('#CreateNewCustomerButton').click(function (e) {
        e.preventDefault();
        _createCustomerModal.open();
    });
    var _utilisationModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Customer/CustomerUtilisationModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customer/_Utilisation.js',
        modalClass: 'CustomerUtilisationModal'
    });

    var dataTable = _$customersTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _customerService.getCustomer,
            inputFilter: function () {
                return createRequestParams();
            }
        },
        columnDefs: [
            {
                targets: 0,
                data: null,
                orderable: false,
                autoWidth: false,
                defaultContent: '',
                rowAction: {
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [
                        {
                            text: app.localize('Edit'),
                            action: function (data) {
                                _createCustomerModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Dashboard'),
                            action: function (data) {
                                _dashboardModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Utilisation'),
                            action: function (data) {
                                _utilisationModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Services'),
                            action: function (data) {
                                debugger;
                                _customerServicesModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('DownloadExcel'),
                            action: function (data) {
                                _customerService
                                    .getCustomersToExcel({ id: data.record.id })
                                    .done(function (result) {
                                        app.downloadTempFile(result);
                                    });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteCustomer(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "customerName"
            },
            {
                targets: 2,
                data: "address1"
            },
            {
                targets: 3,
                data: "address2"
            },
            {
                targets: 4,
                data: "zipCode"
            },
            {
                targets: 5,
                data: "city"
            },
            {
                targets: 6,
                data: "countryName"
            },
            {
                targets: 7,
                data: "phone1"
            },
            {
                targets: 8,
                data: "phone2"
            },
            {
                targets: 9,
                data: "segmentName"
            },
            {
                targets: 10,
                data: "creationTime",
                render: function (creationTime) {
                    return moment(creationTime).format('L');
                }
            }
        ]
    });
    function getCustomers() {

        dataTable.ajax.reload();
    }
    function createRequestParams() {
        var prms = {};
        _$filterForm.serializeArray().map(function (x) { prms[abp.utils.toCamelCase(x.name)] = x.value; });
        return prms;
    }
    function deleteCustomer(customer) {

        abp.message.confirm(
            "Are you sure you want to delete customer?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _customerService.deleteCustomer({
                        id: customer.id
                    }).done(function () {
                        getCustomers(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#CustomerFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#CustomerFiltersArea').slideUp();
    });

    $('#GetCustomersButton, #RefreshCustomerListButton').click(function (e) {
        e.preventDefault();
        getCustomers();
    });

})();