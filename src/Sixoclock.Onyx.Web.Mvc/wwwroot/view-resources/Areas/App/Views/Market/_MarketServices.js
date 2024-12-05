//var editor;

(function () {
    app.modals.MarketServicesModel = function () {

        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$marketServicesTable = $('#MarketServicesTable');
        var _marketServicesService = abp.services.app.marketService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
        var _createOrEditMarketServiceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Markets/CreateOrEditMarketServiceModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Market/CreateOrEditMarketService.js',
            modalClass: 'CreateOrEditMarketServiceModal'
        });
        var _MarketServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Markets/MarketServicePriceParameterModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Market/_PriceParameters.js',
            modalClass: 'MarketServicePriceParameterModel'
        });
        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditPriceParameterModal'
        //});
       debugger
        var marketId = $("#MarketId").val();
        var dataTable = _$marketServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                debugger;
                _marketServicesService.getMarketServices({ id:marketId }).done(function (result) {

                    _$marketServicesTable.data = result.services;
                    callback(_$marketServicesTable);
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
                                    _MarketServicePriceParameterModal.open({ id: data.record.id });
                                    //_createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteMarketService(data.record);
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

        function deleteMarketService(MarketService) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', MarketService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _marketServicesService.deleteService({
                            id: MarketService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateMarketServiceButton').click(function () {

            _createOrEditMarketServiceModal.open({ id: 0, marketId: marketId });
        });

        $('#RefreshPriceParametersButton').click(function (e) {
            e.preventDefault();
            getServicePriceParameters();
        });

        function getServicePriceParameters() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditMarketServiceModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();