(function () {

    var _$otherOptionsTable = $('#OtherOptionsTable');
    var _$filterForm = $('#OtherOptionFilterForm');
    var _otherOptionService = abp.services.app.otherOption;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/OtherOptions/CreateOrEditOtherOptionModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/OtherOptions/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditOtherOptionModal'
    });

    $('#CreateNewOtherOptionButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$otherOptionsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _otherOptionService.getOtherOption,
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
                                deleteOtherOption(data.record);
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
    function getOtherOptions(reload) {
        
        dataTable.ajax.reload();
    }

    function deleteOtherOption(otherOption) {

        abp.message.confirm(
            "Are you sure you want to delete otherOption?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _otherOptionService.deleteOtherOption({
                        id: otherOption.id
                    }).done(function () {
                        getOtherOptions(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#OtherOptionFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#OtherOptionFiltersArea').slideUp();
    });

    $('#GetOtherOptionsButton, #RefreshOtherOptionsListButton').click(function (e) {
        e.preventDefault();
        getOtherOptions();
    });
    
})();