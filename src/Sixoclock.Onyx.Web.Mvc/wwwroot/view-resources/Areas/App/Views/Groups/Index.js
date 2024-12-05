(function () {

    var _$groupsTable = $('#GroupsTable');
    var _$filterForm = $('#GroupFilterForm');
    var _groupService = abp.services.app.group;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Groups/CreateOrEditGroupModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Groups/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditGroupModal'
    });

    var _groupServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Groups/GroupServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Groups/_GroupServices.js',
        modalClass: 'GroupServicesModel'
    });

    var _dashboardModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Groups/GroupDashboardModal'
    });
    var _utilisationModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Groups/GroupUtilisationModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Groups/_Utilisation.js',
        modalClass: 'GroupUtilisationModal'
    });

    $('#CreateNewGroupButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });

    var dataTable = _$groupsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _groupService.getGroup,
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
                            text: app.localize('Dashboard'),
                            action: function (data) {
                                _dashboardModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Utilisation'),
                            action: function (data) {
                                _utilisationModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Services'),
                            action: function (data) {
                                debugger;
                                _groupServicesModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('DownloadExcel'),
                            action: function (data) {
                                _groupService
                                    .getGroupsToExcel({ id: data.record.id })
                                    .done(function (result) {
                                        app.downloadTempFile(result);
                                    });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteGroup(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "groupName"
            },
            {
                targets: 2,
                data: "installName"
            },
            {
                targets: 3,
                data: "countryName"
            },
            {
                targets: 4,
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
    function getGroups() {

        dataTable.ajax.reload();
    }

    function deleteGroup(group) {

        abp.message.confirm(
            "Are you sure you want to delete group?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _groupService.deleteGroup({
                        id: group.id
                    }).done(function () {
                        getGroups(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#GroupFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#GroupFiltersArea').slideUp();
    });

    $('#GetGroupsButton, #RefreshGroupsListButton').click(function (e) {
        e.preventDefault();
        getGroups();
    });

})();