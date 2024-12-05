(function () {

    var _$connectorsTable = $('#ConnectorsTable');
    var _$filterForm = $('#ConnectorFilterForm');
    var _connectorService = abp.services.app.connector;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Connectors/CreateOrEditConnectorModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Connectors/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditConnectorModal'
    });

    var dataTable = _$connectorsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _connectorService.getConnector,
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
                            visible: function (data) {
                                return false;
                            },
                            action: function (data) {
                                deleteConnector(data.record);
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
                data: "evseId"
            },
            {
                targets: 4,
                data: "connectorType"
            },
            {
                targets: 5,
                data: "comment"
            },
            {
                targets: 6,
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
    function getConnectors() {
        
        dataTable.ajax.reload();
    }

    function deleteConnector(connector) {

        _connectorService.getConnectorNo({
            id: connector.chargepointId
        }).done(function (result) {

            if (connector.connectorNumber < result - 1) {
                sweetAlert("You can't delete selected connector because it will break the series.")
                return;
            }

            abp.message.confirm(
                "Are you sure you want to delet model connector?",
                function (isConfirmed) {

                    if (isConfirmed) {
                        _connectorService.deleteConnector({
                            id: connector.id
                        }).done(function () {
                            getConnectors(true);
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
        $('#ConnectorFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ConnectorFiltersArea').slideUp();
    });

    $('#GetConnectorsButton, #RefreshConnectorsListButton').click(function (e) {
        e.preventDefault();
        getConnectors();
    });

})();