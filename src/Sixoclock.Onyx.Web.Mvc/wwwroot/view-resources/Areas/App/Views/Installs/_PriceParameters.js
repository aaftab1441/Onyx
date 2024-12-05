//var editor;

(function () {
    app.modals.InstallServicePriceParameterModel = function () {
        debugger
        var _modalManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$installServicePriceParameterTable = $('#PriceParametersTable');
        var _installServicePriceParameterService = abp.services.app.installServicePriceParameter;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        var _createOrEditInstallServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Installs/CreateOrEditInstallServicePriceParameterModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Installs/_EditServicePriceParameterModal.js',
            modalClass: 'CreateOrEditPriceParameterModal'
        });
       
        var installServiceId = $("#InstallServiceId").val();
        var dataTable = _$installServicePriceParameterTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                
                _installServicePriceParameterService.getServicePriceParameters(installServiceId).done(function (result) {
                  
                    _$installServicePriceParameterTable.data = result.priceParameters;
                    callback(_$installServicePriceParameterTable);
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
                                    _createOrEditInstallServicePriceParameterModal.open({ id: data.record.id, serviceId: installServiceId });
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
                    data: "value"
                }
                
            ]
           
        });


        $('#CreateInstallServiceButton').click(function () {
          
            _createOrEditInstallServiceModal.open({ id: 0, installId: installId});
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