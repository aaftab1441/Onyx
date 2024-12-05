(function () {

    var _$modelKeyValuesTable = $('#ModelKeyValuesTable');
    var _$filterForm = $('#ModelKeyValueFilterForm');
    var _modelKeyValuesService = abp.services.app.modelKeyValue;
    var _editModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ModelOCPPConfigs/EditModelKeyValueModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ModelOCPPConfigs/_EditModal.js',
        modalClass: 'EditModelKeyValuesModal'
    });
    
    var dataTable = _$modelKeyValuesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _modelKeyValuesService.getModelKeyValue,
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
                                deleteModelKeyValue(data.record);
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
                data: "versionName"
            },
            {
                targets: 4,
                data: "featureName"
            },
            {
                targets: 5,
                data: "key"
            },
            {
                targets: 6,
                data: "modelValue"
            },
            {
                targets: 7,
                data: "rw"
            },
            {
                targets: 8,
                data: "comment",
                render: function (data, type, full, meta) {
                    
                    return "<div style='white-space:normal;width:100%'>" + data + "</div>";
                }
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
    function getModelKeyValues() {
        
        dataTable.ajax.reload();
    }
    function deleteModelKeyValue(config) {

        //not implemented
    }
    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ModelKeyValueFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ModelKeyValueFiltersArea').slideUp();
    });

    $('#GetModelKeyValuesButton, #RefreshModelKeyValuesListButton').click(function (e) {
        e.preventDefault();
        getModelKeyValues();
    });
    
})();