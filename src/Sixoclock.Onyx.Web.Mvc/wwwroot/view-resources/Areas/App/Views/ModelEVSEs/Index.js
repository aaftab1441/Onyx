(function () {

    var _$modelEVSEsTable = $('#ModelEVSEsTable');
    var _$filterForm = $('#ModelEVSEFilterForm');
    var _modelEVSEService = abp.services.app.modelEVSE;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ModelEVSEs/CreateOrEditModelEVSEModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ModelEVSEs/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditModelEVSEModal'
    });

    $('#CreateNewModelEVSEButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$modelEVSEsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _modelEVSEService.getModelEVSE,
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
                                deleteModelEVSE(data.record);
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
                data: "comment"
            },
            {
                targets: 4,
                data: "evseId"
            },
            {
                targets: 5,
                data: "connectorsCount"
            },
            {
                targets: 6,
                data: "meterType"
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
    function getModelEVSEs() {
        
        dataTable.ajax.reload();
    }

    function deleteModelEVSE(modelEVSE) {
        
        _modelEVSEService.getEVSEIdCount({
            id: modelEVSE.chargepointModelId
        }).done(function (result) {
            
            if (modelEVSE.evseId < result - 1) {
                sweetAlert("You can't delete selected model evse because it will break the series.")
                return;
            }

            abp.message.confirm(
                "Are you sure you want to delet model EVSE?",
                function (isConfirmed) {

                    if (isConfirmed) {
                        _modelEVSEService.deleteModelEVSE({
                            id: modelEVSE.id
                        }).done(function () {
                            getModelEVSEs(true);
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
        $('#ModelEVSEFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ModelEVSEFiltersArea').slideUp();
    });

    $('#GetModelEVSEsButton, #RefreshModelEVSEsListButton').click(function (e) {
        e.preventDefault();
        getModelEVSEs();
    });
    
})();