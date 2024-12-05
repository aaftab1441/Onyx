//var editor;

(function () {
    app.modals.SegmentServicesModel = function () {

        var _modalManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$segmentServicesTable = $('#SegmentServicesTable');
        var _segmentServicesService = abp.services.app.segmentService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };

        var _createOrEditSegmentServiceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Segments/CreateOrEditSegmentServiceModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Segments/CreateOrEditSegmentService.js',
            modalClass: 'CreateOrEditSegmentServiceModal'
        });
        var _SegmentServicePriceParameterModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Segments/SegmentServicePriceParameterModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Segments/_PriceParameters.js',
            modalClass: 'SegmentServicePriceParameterModel'
        });
       
        var segmentId = $("#SegmentId").val();
        var dataTable = _$segmentServicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                
                _segmentServicesService.getSegmentServices({ id:segmentId }).done(function (result) {
                   
                    _$segmentServicesTable.data = result.services;
                    callback(_$segmentServicesTable);
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
                                    debugger;
                                    _SegmentServicePriceParameterModal.open({ id: data.record.id});
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
                    data: "description"
                }
                , {
                    targets: 3,
                    data: "pricingParamsCount"
                }
            ]
           
        });

        function deleteSegmentService(segmentService) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', segmentService.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _segmentServicesService.deleteService({
                            id: segmentService.id
                        }).done(function () {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

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

        abp.event.on('app.createOrEditSegmentServiceModalSaved',
            function () {
                getServicePriceParameters();
            });
    }

})();