(function () {

    var _$adminStatusesTable = $('#AdminStatusesTable');
    var _$filterForm = $('#AdminStatusFilterForm');
    var _adminStatusService = abp.services.app.adminStatus;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/AdminStatuses/CreateOrEditAdminStatusModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/AdminStatuses/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditAdminStatusModal'
    });

    $('#CreateNewAdminStatusButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });

    var dataTable = _$adminStatusesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _adminStatusService.getAdminStatus,
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
                                deleteAdminStatus(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "status"
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
    function getAdminStatuses(reload) {

        if (reload) {
            dataTable.ajax.reload();
        } else {
            dataTable.ajax.reload({
                filter: $('#AdminStatusesTableFilter').val(),
                name: $("#name").val(),
                comment: $("#comment").val()
            });
        }
    }

    function deleteAdminStatus(adminStatus) {

        abp.message.confirm(
            "Are you sure you want to delete admin status code?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _adminStatusService.deleteAdminStatus({
                        id: adminStatus.id
                    }).done(function () {
                        getAdminStatuses(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#AdminStatusFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#AdminStatusFiltersArea').slideUp();
    });

    $('#GetAdminStatusesButton, #RefreshAdminStatusesListButton').click(function (e) {
        e.preventDefault();
        getAdminStatuses();
    });

})();