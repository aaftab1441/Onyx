//var editor;

(function () {
    app.modals.CustomerServicePriceParameterModel = function () {

        var _modalManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$customerServicePriceParameterTable = $('#PriceParametersTable');
        var _customerServicePriceParameterService = abp.services.app.customerServicePriceParameter;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        var _createOrEditCustomerServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Customer/CreateOrEditCustomerServicePriceParameterModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customer/_EditServicePriceParameterModal.js',
            modalClass: 'CreateOrEditPriceParameterModal'
        });
       
        var customerServiceId = $("#CustomerServiceId").val();
        var dataTable = _$customerServicePriceParameterTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                
                _customerServicePriceParameterService.getServicePriceParameters(customerServiceId).done(function (result) {
                  
                    _$customerServicePriceParameterTable.data = result.priceParameters;
                    callback(_$customerServicePriceParameterTable);
                });
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: true,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-xs btn-primary blue',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    debugger;
                                    _createOrEditCustomerServicePriceParameterModal.open({ id: data.record.id, serviceId: customerServiceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteCustomerService(data.record);
                                }
                            }
                        ]
                    }
                }, {
                    targets: 1,
                    data: "name"
                }, {
                    targets: 2,
                    data: "value"
                }
                
            ]
           
        });


        $('#CreateCustomerServiceButton').click(function () {
          
            _createOrEditCustomerServiceModal.open({ id: 0, customerId: customerId});
        });

        $('#RefreshPriceParametersButton').click(function (e) {
            e.preventDefault();
            getServicePriceParameters();
        });

        function getServicePriceParameters() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.CreateOrEditPriceParameterModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();