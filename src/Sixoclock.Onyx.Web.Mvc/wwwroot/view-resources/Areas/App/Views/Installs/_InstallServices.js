//var editor;

(function () {
    app.modals.InstallServicesModel = function () {

        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$installServicesTable = $('#InstallServicesTable');
        var _installServicesService = abp.services.app.installService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
        var _createOrEditInstallServiceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Installs/CreateOrEditInstallServiceModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Installs/CreateOrEditInstallService.js',
            modalClass: 'CreateOrEditInstallServiceModal'
        });
        var _InstallServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Installs/InstallServicePriceParameterModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Installs/_PriceParameters.js',
            modalClass: 'InstallServicePriceParameterModel'
        });
        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditPriceParameterModal'
        //});
       
        var installId = $("#InstallId").val();
        var dataTable = _$installServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                debugger;
                _installServicesService.getInstallServices({ id:installId }).done(function (result) {

                    _$installServicesTable.data = result.services;
                    callback(_$installServicesTable);
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
                                    debugger
                                    _InstallServicePriceParameterModal.open({ id: data.record.id });

                                    //_createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteInstallService(data.record);
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

        function deleteInstallService(installService) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', installService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _installServicesService.deleteService({
                            id: installService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateInstallServiceButton').click(function () {

            _createOrEditInstallServiceModal.open({ id: 0, installId: installId });
        });

        $('#RefreshPriceParametersButton').click(function (e) {
            e.preventDefault();
            getServicePriceParameters();
        });

        function getServicePriceParameters() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditInstallServiceModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();