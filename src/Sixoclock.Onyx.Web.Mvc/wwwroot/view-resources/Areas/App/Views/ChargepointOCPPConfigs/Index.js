(function () {

    var _$chargepointKeyValuesTable = $('#ChargepointKeyValuesTable');
    var _$filterForm = $('#ChargepointKeyValueFilterForm');
    var _chargepointKeyValuesService = abp.services.app.chargepointKeyValue;
    var _editModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ChargepointOCPPConfigs/EditChargepointKeyValueModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ChargepointOCPPConfigs/_EditModal.js',
        modalClass: 'EditChargepointKeyValuesModal'
    });
    
    var dataTable = _$chargepointKeyValuesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _chargepointKeyValuesService.getChargepointKeyValue,
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
                                
                                _editModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Delete'),
                            visible: function (data) {
                                return false;
                            },
                            action: function (data) {
                                deleteChargepointKeyValue(data.record);
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
                data: "identity"
            },
            {
                targets: 4,
                data: "versionName"
            },
            {
                targets: 5,
                data: "featureName"
            },
            {
                targets: 6,
                data: "key"
            },
            {
                targets: 7,
                data: "chargepointValue"
            },
            {
                targets: 8,
                data: "rw"
            },
            {
                targets: 9,
                data: "comment",
                render: function (data, type, full, meta) {

                    return "<div style='white-space:normal;width:100%'>" + data + "</div>";
                }
            },
            {
                targets: 10,
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
    function getChargepointKeyValues() {
        
        dataTable.ajax.reload();
    }
    function deleteChargepointKeyValue(config) {

        abp.message.confirm(
            "Are you sure you want to delete key value?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _chargepointKeyValuesService.deleteChargepointKeyValue({
                        id: config.id
                    }).done(function () {
                        getConfigs(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }
    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ChargepointKeyValueFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ChargepointKeyValueFiltersArea').slideUp();
    });

    $('#GetChargepointKeyValuesButton, #RefreshChargepointKeyValuesListButton').click(function (e) {
        e.preventDefault();
        getChargepointKeyValues();
    });

})();