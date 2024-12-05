//var editor;

(function () {
    app.modals.GroupServicePriceParameterModel = function () {

        var _modalManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$groupServicePriceParameterTable = $('#PriceParametersTable');
        var _groupServicePriceParameterService = abp.services.app.groupServicePriceParameter;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        var _createOrEditGroupServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Groups/CreateOrEditGroupServicePriceParameterModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Groups/_EditServicePriceParameterModal.js',
            modalClass: 'CreateOrEditPriceParameterModal'
        });
       
        var groupServiceId = $("#GroupServiceId").val();
        var dataTable = _$groupServicePriceParameterTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                
                _groupServicePriceParameterService.getServicePriceParameters(groupServiceId).done(function (result) {
                  
                    _$groupServicePriceParameterTable.data = result.priceParameters;
                    callback(_$groupServicePriceParameterTable);
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
                                    _createOrEditGroupServicePriceParameterModal.open({ id: data.record.id, serviceId: groupServiceId });
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
                    data: "value"
                }
                
            ]
           
        });


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

        abp.event.on('app.CreateOrEditPriceParameterModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();