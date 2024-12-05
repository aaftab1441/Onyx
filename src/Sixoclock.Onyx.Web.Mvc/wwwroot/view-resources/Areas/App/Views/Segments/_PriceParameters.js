//var editor;

(function () {
    app.modals.SegmentServicePriceParameterModel = function () {

        var _modalManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$segmentServicePriceParameterTable = $('#PriceParametersTable');
        var _segmentServicePriceParameterService = abp.services.app.segmentServicePriceParameter;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        var _createOrEditSegmentServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Segments/CreateOrEditSegmentServicePriceParameterModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Segments/_EditServicePriceParameterModal.js',
            modalClass: 'CreateOrEditPriceParameterModal'
        });
       
        var segmentServiceId = $("#SegmentServiceId").val();
        var dataTable = _$segmentServicePriceParameterTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                
                _segmentServicePriceParameterService.getServicePriceParameters(segmentServiceId).done(function (result) {
                  
                    _$segmentServicePriceParameterTable.data = result.priceParameters;
                    callback(_$segmentServicePriceParameterTable);
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
                                    _createOrEditSegmentServicePriceParameterModal.open({ id: data.record.id, serviceId: segmentServiceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteSegmentService(data.record);
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


        $('#CreateSegmentServiceButton').click(function () {
          
            _createOrEditSegmentServiceModal.open({ id: 0, segmentId: segmentId});
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