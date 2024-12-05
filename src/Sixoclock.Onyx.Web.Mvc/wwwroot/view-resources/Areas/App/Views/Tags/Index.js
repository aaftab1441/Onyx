(function () {

    var _$tagsTable = $('#TagsTable');
    var _$filterForm = $('#TagFilterForm');
    var _tagService = abp.services.app.tag;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Tags/CreateOrEditTagModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Tags/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditTagModal'
    });
    var _viewDetailsModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Tags/ViewDetailsTagModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Tags/_ViewDetails.js',
        modalClass: 'CreateOrEditTransactionModal'
    });

    $('#CreateNewTagButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$tagsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _tagService.getTag,
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
                            text: app.localize('ViewDetails'),
                            action: function (data) {
                                _viewDetailsModal.open({ id: data.record.id });
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "idToken"
            },
            {
                targets: 2,
                data: "parentTagId"
            },
            {
                targets: 3,
                data: "userName"
            },
            {
                targets: 4,
                data: "authorizationStatus"
            },
            {
                targets: 5,
                data: "expiry"
            },
            {
                targets: 6,
                data: "comment"
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
    function getTags(reload) {

        dataTable.ajax.reload();
    }

    function deleteTag(tag) {

        abp.message.confirm(
            "Are you sure you want to delete tag?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _tagService.deleteTag({
                        id: tag.id
                    }).done(function () {
                        getTags(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#TagFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#TagFiltersArea').slideUp();
    });

    $('#GetTagsButton, #RefreshTagsListButton').click(function (e) {
        e.preventDefault();
        getTags();
    });
    
})();