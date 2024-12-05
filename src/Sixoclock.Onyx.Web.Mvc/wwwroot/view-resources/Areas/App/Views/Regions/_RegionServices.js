//var editor;

(function () {
    app.modals.RegionServicesModel = function () {
        debugger
        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$regionServicesTable = $('#RegionServicesTable');
        var _regionServicesService = abp.services.app.regionService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
        var _createOrEditRegionServiceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Regions/CreateOrEditRegionServiceModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Regions/CreateOrEditRegionService.js',
            modalClass: 'CreateOrEditRegionServiceModal'
        });
        var _RegionServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Regions/RegionServicePriceParameterModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Regions/_PriceParameters.js',
            modalClass: 'RegionServicePriceParameterModel'
        });
        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditPriceParameterModal'
        //});
       
        var regionId = $("#RegionId").val();
        var dataTable = _$regionServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                debugger;
                _regionServicesService.getRegionServices({ id: regionId }).done(function (result) {

                    _$regionServicesTable.data = result.services;
                    callback(_$regionServicesTable);
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
                                    _RegionServicePriceParameterModal.open({ id: data.record.id });

                                    //_createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteRegionService(data.record);
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

        function deleteRegionService(regionService) {
            debugger
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', regionService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _regionServicesService.deleteService({
                            id: regionService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateRegionServiceButton').click(function () {

            _createOrEditRegionServiceModal.open({ id: 0, regionId: regionId});
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