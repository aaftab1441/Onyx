(function () {

    var _$chargepointModelsTable = $('#ChargepointModelsTable');
    var _$filterForm = $('#ChargepointModelFilterForm');
    var _chargepointModelService = abp.services.app.chargePointModel;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ChargepointModels/CreateOrEditChargepointModelModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ChargepointModels/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditChargepointModelModal'
    });

    $('#CreateNewChargepointModelButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$chargepointModelsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _chargepointModelService.getChargepointModel,
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
                            text: app.localize('Edit'),
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteChargepointModel(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "vendorName"
            },
            {
                targets: 2,
                data: "modelName"
            },
            {
                targets: 3,
                data: "comment"
            },
            {
                targets: 4,
                data: "mountName"
            },
            {
                targets: 5,
                data: "modelEvseCount"
            },
            {
                targets: 6,
                data: "modelConnectorsCount"
            },
            {
                targets: 7,
                data: "versionName"
            },
            {
                targets: 8,
                data: "ocppTransportName"
            },
            {
                targets: 9,
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
    function getChargepointModels() {
        
        dataTable.ajax.reload();
    }

    function deleteChargepointModel(chargepointModel) {

        abp.message.confirm(
            "Are you sure you want to delete chargepointmodel?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _chargepointModelService.deleteChargepointModel({
                        id: chargepointModel.id
                    }).done(function () {
                        getChargepointModels();
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ChargepointModelFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ChargepointModelFiltersArea').slideUp();
    });

    $('#GetChargepointModelsButton, #RefreshChargepointModelListButton').click(function (e) {
        e.preventDefault();
        getChargepointModels();
    });

})();