(function () {

    var _$connectorStatusCodesTable = $('#ConnectorStatusCodesTable');
    var _$filterForm = $('#ConnectorStatusCodeFilterForm');
    var _connectorStatusCodeService = abp.services.app.connectorStatusCode;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ConnectorStatusCodes/CreateOrEditConnectorStatusCodeModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ConnectorStatusCodes/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditConnectorStatusCodeModal'
    });

    $('#CreateNewConnectorStatusCodeButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$connectorStatusCodesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _connectorStatusCodeService.getConnectorStatusCode,
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
                                deleteConnectorStatusCode(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "status"
            },
            {
                targets: 2,
                data: "comment"
            },
            {
                targets: 3,
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
    function getConnectorStatusCodes() {
        
        dataTable.ajax.reload();
    }

    function deleteConnectorStatusCode(connectorStatusCode) {

        abp.message.confirm(
            "Are you sure you want to delete status code?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _connectorStatusCodeService.deleteConnectorStatusCode({
                        id: connectorStatusCode.id
                    }).done(function () {
                        getConnectorStatusCodes(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ConnectorStatusCodeFilterFormFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ConnectorStatusCodeFilterFormFiltersArea').slideUp();
    });

    $('#GetConnectorStatusCodesButton, #RefreshConnectorStatusCodesListButton').click(function (e) {
        e.preventDefault();
        getConnectorStatusCodes();
    });

})();