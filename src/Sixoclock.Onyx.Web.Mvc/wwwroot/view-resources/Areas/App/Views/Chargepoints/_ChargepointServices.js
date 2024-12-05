//var editor;

(function () {
    app.modals.ChargepointServicesModel = function () {

        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$chargepointServicesTable = $('#ChargepointServicesTable');
        var _chargepointServicesService = abp.services.app.chargepointService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditPriceParameterModal'
        //});
       
        var ChargepointId = $("#ChargepointId").val();
        var dataTable = _$chargepointServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                debugger;
                _chargepointServicesService.getChargepointServices({ id:ChargepointId }).done(function (result) {

                    _$chargepointServicesTable.data = result.services;
                    callback(_$chargepointServicesTable);
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

                                    //_createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteChargepointService(data.record);
                                }
                            }
                        ]
                    }
                }, {
                    targets: 1,
                    data: "name"
                }, {
                    targets: 2,
                    data: "Description"
                }
                , {
                    targets: 3,
                    data: "PricingParamsCount"
                }
            ]
           
        });

        function deleteChargepointService(ChargepointService) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', ChargepointService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _chargepointServicesService.deleteService({
                            id: ChargepointService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewPriceParameterButton').click(function () {

            _createOrEditModal.open({ id: 0, serviceId: serviceId });
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