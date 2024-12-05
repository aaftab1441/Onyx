//var editor;

(function () {
    app.modals.RegionServicePriceParameterModel = function () {

        var _modalManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$regionServicePriceParameterTable = $('#PriceParametersTable');
        var _regionServicePriceParameterService = abp.services.app.regionServicePriceParameter;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        var _createOrEditRegionServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Regions/CreateOrEditRegionServicePriceParameterModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Regions/_EditServicePriceParameterModal.js',
            modalClass: 'CreateOrEditPriceParameterModal'
        });
       
        var regionServiceId = $("#RegionServiceId").val();
        var dataTable = _$regionServicePriceParameterTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                
                _regionServicePriceParameterService.getServicePriceParameters(regionServiceId).done(function (result) {
                  
                    _$regionServicePriceParameterTable.data = result.priceParameters;
                    callback(_$regionServicePriceParameterTable);
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
                                    _createOrEditRegionServicePriceParameterModal.open({ id: data.record.id, serviceId: regionServiceId });
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
                    data: "value"
                }
                
            ]
           
        });


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