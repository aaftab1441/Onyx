(function () {

    var _$chargeReleaseOptionsTable = $('#ChargeReleaseOptionsTable');
    var _chargeReleaseOptionService = abp.services.app.chargeReleaseOption;
    var _$filterForm = $('#ReleaseOptionFilterForm');
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ChargeReleaseOptions/CreateOrEditChargeReleaseOptionModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ChargeReleaseOptions/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditChargeReleaseOptionModal'
    });

    $('#CreateNewChargeReleaseOptionButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$chargeReleaseOptionsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _chargeReleaseOptionService.getChargeReleaseOption,
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
                                deleteChargeReleaseOption(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "option"
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
    function getChargeReleaseOptions() {
        
        if (reload) {
            dataTable.ajax.reload();
        } else {
            dataTable.ajax.reload({
                filter: $('#ChargeReleaseOptionsTableFilter').val(),
                option: $("#option").val(),
                modelName: $("#modelName").val()
            });
        }
    }

    function deleteChargeReleaseOption(chargeReleaseOption) {

        abp.message.confirm(
            "Are you sure you want to delete chargeReleaseOption?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _chargeReleaseOptionService.deleteChargeReleaseOption({
                        id: chargeReleaseOption.id
                    }).done(function () {
                        getChargeReleaseOptions(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#AdvacedAuditFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#AdvacedAuditFiltersArea').slideUp();
    });

    $('#GetChargeReleaseOptionsButton, #RefreshChargeReleaseOptionsListButton').click(function (e) {
        e.preventDefault();
        getChargeReleaseOptions();
    });

})();