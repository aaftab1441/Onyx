(function () {

    var _$meterTypesTable = $('#MeterTypesTable');
    var _$filterForm = $('#MeterTypeFilterForm');
    var _meterTypeService = abp.services.app.meterType;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/MeterTypes/CreateOrEditMeterTypeModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/MeterTypes/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditMeterTypeModal'
    });

    $('#CreateNewMeterTypeButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });

    var dataTable = _$meterTypesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _meterTypeService.getMeterType,
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
                                deleteMeterType(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "type"
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
    function getMeterTypes() {
        
        dataTable.ajax.reload();
    }

    function deleteMeterType(meterType) {

        abp.message.confirm(
            "Are you sure you want to delete meterType?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _meterTypeService.deleteMeterType({
                        id: meterType.id
                    }).done(function () {
                        getMeterTypes(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#MeterTypeFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#MeterTypeFiltersArea').slideUp();
    });

    $('#GetMeterTypesButton, #RefreshMeterTypesListButton').click(function (e) {
        e.preventDefault();
        getMeterTypes();
    });
    
})();