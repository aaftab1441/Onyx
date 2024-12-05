(function () {

    var _$eVSEsTable = $('#EVSEsTable');
    var _$filterForm = $('#EVSEFilterForm');
    var _eVSEService = abp.services.app.eVSE;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/EVSEs/CreateOrEditEVSEModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/EVSEs/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditEVSEModal'
    });

    $('#CreateNewModelEVSEButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$eVSEsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _eVSEService.getEVSE,
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
                                deleteEVSE(data.record);
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
                data: "evsE_id"
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
                data: "availabilityType"
            },
            {
                targets: 8,
                data: "evseStatus"
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
    function getEVSEs() {
        
        dataTable.ajax.reload();
    }

    function deleteEVSE(eVSE) {

        _eVSEService.getEVSEIdCount({
            id: eVSE.chargepointId
        }).done(function (result) {
            
            if (eVSE.evsE_id < result - 1) {
                sweetAlert("You can't delete selected EVSE because it will break the series.")
                return;
            }

            abp.message.confirm(
                "Are you sure you want to delete this EVSE?",
                function (isConfirmed) {

                    if (isConfirmed) {
                        _eVSEService.deleteEVSE({
                            id: eVSE.id
                        }).done(function () {
                            getEVSEs(true);
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
        $('#EVSEFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#EVSEFiltersArea').slideUp();
    });

    $('#GetEVSEsButton, #RefreshEVSEsListButton').click(function (e) {
        e.preventDefault();
        getEVSEs();
    });
    
})();