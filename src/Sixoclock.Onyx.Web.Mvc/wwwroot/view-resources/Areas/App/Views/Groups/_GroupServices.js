//var editor;

(function () {
    app.modals.GroupServicesModel = function () {

        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$groupServicesTable = $('#GroupServicesTable');
        var _groupServicesService = abp.services.app.groupService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
        var _createOrEditGroupServiceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Groups/CreateOrEditGroupServiceModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Groups/CreateOrEditGroupService.js',
            modalClass: 'CreateOrEditGroupServiceModal'
        });
        var _GroupServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Groups/GroupServicePriceParameterModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Groups/_PriceParameters.js',
            modalClass: 'GroupServicePriceParameterModel'
        });
        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditPriceParameterModal'
        //});
       
        var groupId = $("#GroupId").val();
        var dataTable = _$groupServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                debugger;
                _groupServicesService.getGroupServices({ id:groupId }).done(function (result) {

                    _$groupServicesTable.data = result.services;
                    callback(_$groupServicesTable);
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
                                    _GroupServicePriceParameterModal.open({ id: data.record.id });

                                    //_createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteGroupService(data.record);
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

        function deleteGroupService(GroupService) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', GroupService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _groupServicesService.deleteService({
                            id: GroupService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateGroupServiceButton').click(function () {

            _createOrEditGroupServiceModal.open({ id: 0, groupId: groupId});
        });

        $('#RefreshPriceParametersButton').click(function (e) {
            e.preventDefault();
            getServicePriceParameters();
        });

        function getServicePriceParameters() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditGroupServiceModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();