//var editor;

(function () {
    app.modals.CustomerServicesModel = function () {
        debugger
        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$customerServicesTable = $('#CustomerServicesTable');
        var _customerServicesService = abp.services.app.customerService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
        debugger
        var _createOrEditCustomerServiceModal = new app.ModalManager({            
            viewUrl: abp.appPath + 'App/Customer/CreateOrEditCustomerServiceModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customer/CreateOrEditCustomerService.js',
            modalClass: 'CreateOrEditCustomerServiceModal'
        });
        var _CustomerServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Customer/CustomerServicePriceParameterModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customer/_PriceParameters.js',
            modalClass: 'CustomerServicePriceParameterModel'
        });
        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditPriceParameterModal'
        //});
       
        var customerId = $("#CustomerId").val();
        var dataTable = _$customerServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                debugger;
                _customerServicesService.getCustomerServices({ id:customerId }).done(function (result) {

                    _$customerServicesTable.data = result.services;
                    callback(_$customerServicesTable);
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
                                text: app.localize('EditPriceParameters'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {

                                    _CustomerServicePriceParameterModal.open({ id: data.record.id });

                                    //_createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
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
                    data: "description"
                }
                , {
                    targets: 3,
                    data: "pricingParamsCount"
                }
            ]
           
        });

        function deleteCustomerService(customerService) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', customerService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _customerServicesService.deleteService({
                            id: customerService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateCustomerServiceButton').click(function () {
            debugger
            _createOrEditCustomerServiceModal.open({ id: 0, customerId: customerId });
           
        });

        $('#RefreshPriceParametersButton').click(function (e) {
            e.preventDefault();
            getServicePriceParameters();
        });

        function getServicePriceParameters() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditCustomerServiceModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();