(function () {

    var _$parentTagsTable = $('#ParentTagsTable');
    var _$filterForm = $('#ParentTagFilterForm');
    var _parentTagService = abp.services.app.parentTag;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ParentTags/CreateOrEditParentTagModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ParentTags/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditParentTagModal'
    });

    $('#CreateNewParentTagButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$parentTagsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _parentTagService.getParentTag,
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
                                deleteParentTag(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "value"
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
    function getParentTags(reload) {

        dataTable.ajax.reload();
    }
    function createRequestParams() {
        var prms = {};
        _$filterForm.serializeArray().map(function (x) { prms[abp.utils.toCamelCase(x.name)] = x.value; });
        return prms;
    }
    function deleteParentTag(parentTag) {

        abp.message.confirm(
            "Are you sure you want to delete ParentTag?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _parentTagService.deleteParentTag({
                        id: parentTag.id
                    }).done(function () {
                        getParentTags(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ParentTagFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ParentTagFiltersArea').slideUp();
    });

    $('#GetParentTagsButton, #RefreshParentTagsListButton').click(function (e) {
        e.preventDefault();
        getParentTags();
    });
    
})();