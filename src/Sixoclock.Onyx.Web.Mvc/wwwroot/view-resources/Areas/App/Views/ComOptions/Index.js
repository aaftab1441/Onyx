(function () {

    var _$comOptionsTable = $('#ComOptionsTable');
    var _$filterForm = $('#ComOptionFilterForm');
    var _comOptionService = abp.services.app.comOption;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ComOptions/CreateOrEditComOptionModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ComOptions/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditComOptionModal'
    });

    $('#CreateNewComOptionButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$comOptionsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _comOptionService.getComOption,
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
                                deleteComOption(data.record);
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
    function getComOptions() {
        
        if (reload) {
            dataTable.ajax.reload();
        } else {
            dataTable.ajax.reload({
                filter: $('#ComOptionsTableFilter').val(),
                option: $("#option").val(),
                modelName: $("#modelName").val()
            });
        }
    }

    function deleteComOption(comOption) {

        abp.message.confirm(
            "Are you sure you want to delete comOption?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _comOptionService.deleteComOption({
                        id: comOption.id
                    }).done(function () {
                        getComOptions(true);
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

    $('#GetComOptionsButton, #RefreshComOptionsListButton').click(function (e) {
        e.preventDefault();
        getComOptions();
    });

})();