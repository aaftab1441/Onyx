(function () {
    $(function () {
       
        var _$servicesTable = $('#ServicesTable');
        var _serviceService = abp.services.app.services;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
       
        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Services/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Services/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditServiceModal'
        })
        var _editPriceParameterModel = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ServicePriceParameters',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/Index.js',
            modalClass: 'EditPriceParameterModel'
        });

        var dataTable = _$servicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function(data, callback, settings) {
                _serviceService.getServices().done(function (result) {
                    _$servicesTable.data = result.services;
                    callback(_$servicesTable);
                });
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
                        items: [{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        },{
                            text: app.localize('EditPriceParameter'),
                            action: function (data) {
                                _editPriceParameterModel.open({ id: data.record.id });
                            }
                        },{
                    
                            text: app.localize('Delete'),
                            visible: function (data) {
                                return !data.record.isStatic && _permissions.delete;
                            },
                            action: function (data) {
                                deleteServices(data.record);
                            }
                        }]
                    }
                }, {
                    
                    targets: 1,
                    data: "name"
                }, {
                    targets: 2,
                    data: "featureName"
                }
            ]
        });

        function deleteServices(service) {
            abp.message.confirm(
                app.localize('ServiceDeleteWarningMessage', service.WhatEverver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _serviceService.deleteService({
                            id: service.id
                        }).done(function () {
                            getServices();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewServiceButton').click(function () {
            _createOrEditModal.open();
        });

        $('#RefreshServicesButton').click(function (e) {
            e.preventDefault();
            getServices();
        });

        function getServices() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.CreateOrEditServiceModalSaved', function () {
            getServices();
        });

    });
})();