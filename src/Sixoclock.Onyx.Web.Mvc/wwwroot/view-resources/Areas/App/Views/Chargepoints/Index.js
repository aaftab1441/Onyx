(function () {

    var _$chargepointsTable = $('#ChargepointsTable');
    var _$filterForm = $('#ChargepointFilterForm');
    var _chargepointService = abp.services.app.chargepoint;
    var _chargePointModelService = abp.services.app.chargePointModel;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Chargepoints/CreateOrEditChargepointModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Chargepoints/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditChargepointModal'
    });

    var _chargepointServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Chargepoints/ChargepointServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Chargepoints/_ChargepointServices.js',
        modalClass: 'ChargepointServicesModel'
    });

    var _manageChargepointModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Chargepoints/ManageChargepointModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Chargepoints/_ManageChargepointModal.js',
        modalClass: 'ManageChargepointModal'
    });
    var _overviewModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Chargepoints/OverviewModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Chargepoints/_OverviewModal.js',
        modalClass: 'OverViewModal'
    });
    var _manageOCPPModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Chargepoints/ManageOCPPModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Chargepoints/_ManageOCPPModal.js',
        modalClass: 'ManageOCPPModal'
    });

    $('#CreateNewChargepointButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });

    var dataTable = _$chargepointsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _chargepointService.getChargepoint,
            inputFilter: function () {
                return createRequestParams();
            }
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
                    items: [
                        {
                            text: app.localize('Edit Charge point'),
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Overview'),
                            action: function (data) {
                                _overviewModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Manage OCPP'),
                            action: function (data) {
                                _manageOCPPModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Commands'),
                            action: function (data) {
                                _manageChargepointModal.open({ id: data.record.id });
                            }
                        },{
                            text: app.localize('Services'),
                            action: function (data) {
                                debugger;
                                _chargepointServicesModal.open({ id: data.record.id });
                            }
                        },{
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteChargepoint(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "installName"
            },
            {
                targets: 2,
                data: "groupName"
            },
            {
                targets: 3,
                data: "vendorName"
            },
            {
                targets: 4,
                data: "modelName"
            },
            {
                targets: 5,
                data: "identity"
            },
            {
                targets: 6,
                data: "connectors"
            },
            {
                targets: 7,
                data: "evsEsCount"
            },
            {
                targets: 8,
                data: "place"
            },
            {
                targets: 9,
                data: "comment"
            },
            {
                targets: 10,
                data: "mountTypeName"
            },
            {
                targets: 11,
                data: "status"
            },
            {
                targets: 12,
                data: "creationTime",
                render: function (creationTime) {
                    return moment(creationTime).format('L');
                }
            }
        ]
    });

    function createRequestParams() {
        var prms = {};
        _$filterForm.serializeArray().map(function (x) { prms[abp.utils.toCamelCase(x.name)] = x.value; });
        return prms;
    }
    function getChargepoints() {
        
        dataTable.ajax.reload();
    }

    function deleteChargepoint(chargepoint) {
        
        abp.message.confirm(
            "Are you sure you want to delete chargepoint?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _chargepointService.deleteChargepoint({
                        id: chargepoint.id
                    }).done(function () {
                        getChargepoints(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ChargepointFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ChargepointFiltersArea').slideUp();
    });

    $('#GetChargepointsButton, #RefreshChargepointListButton').click(function (e) {
        e.preventDefault();
        getChargepoints();
    });

})();