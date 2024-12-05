(function () {

    var _$modelConnectorsTable = $('#ModelConnectorsTable');
    var _$filterForm = $('#ModelConnectorFilterForm');
    var _modelConnectorService = abp.services.app.modelConnector;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ModelConnectors/CreateOrEditModelConnectorModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ModelConnectors/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditModelConnectorModal'
    });

    $('#CreateNewModelConnectorButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$modelConnectorsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _modelConnectorService.getModelConnector,
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
                                deleteModelConnector(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "vendor"
            },
            {
                targets: 2,
                data: "modelName"
            },
            {
                targets: 3,
                data: "connectorId"
            },
            {
                targets: 4,
                data: "modelEVSEId"
            },
            {
                targets: 5,
                data: "comment"
            },
            {
                targets: 6,
                data: "connectorType"
            },
            {
                targets: 7,
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
    function getModelConnectors() {
        
        dataTable.ajax.reload();
    }

    function deleteModelConnector(modelConnector) {
        
        _modelConnectorService.getConnectorNo({
            id: modelConnector.record.chargepointModelId
        }).done(function (result) {
                        
            if (modelConnector.record.connectorId < result - 1) {
                sweetAlert("You can't delete selected connector because it will break the series.")
                return;
            }

            abp.message.confirm(
                "Are you sure you want to delet model connector?",
                function (isConfirmed) {

                    if (isConfirmed) {
                        _modelConnectorService.deleteModelConnector({
                            id: modelConnector.record.id
                        }).done(function () {
                            getModelConnectors(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        });
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ModelConnectorFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ModelConnectorFiltersArea').slideUp();
    });

    $('#GetModelConnectorsButton, #RefreshModelConnectorsListButton').click(function (e) {
        e.preventDefault();
        getModelConnectors();
    });
    
})();