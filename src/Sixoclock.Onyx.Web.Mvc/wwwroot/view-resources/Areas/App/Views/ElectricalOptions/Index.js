(function () {

    var _$electricalOptionsTable = $('#ElectricalOptionsTable');
    var _electricalOptionService = abp.services.app.electricalOption;
    var _$filterForm = $('#ElectricalOptionFilterForm');
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ElectricalOptions/CreateOrEditElectricalOptionModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ElectricalOptions/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditElectricalOptionModal'
    });

    $('#CreateNewElectricalOptionButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$electricalOptionsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _electricalOptionService.getElectricalOption,
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
                                deleteElectricalOption(data.record);
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
    function getElectricalOptions() {
        
        dataTable.ajax.reload();
    }

    function deleteElectricalOption(electricalOption) {

        abp.message.confirm(
            "Are you sure you want to deleet electricalOption?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _electricalOptionService.deleteElectricalOption({
                        id: electricalOption.id
                    }).done(function () {
                        getElectricalOptions(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ElectricalOptionFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ElectricalOptionFiltersArea').slideUp();
    });

    $('#GetElectricalOptionsButton, #RefreshElectricalOptionsListButton').click(function (e) {
        e.preventDefault();
        getElectricalOptions();
    });
    
})();