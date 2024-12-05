(function () {

    var _$installsTable = $('#InstallsTable');
    var _$filterForm = $('#InstallFilterForm');
    var _installService = abp.services.app.install;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Installs/CreateOrEditInstallModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Installs/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditInstallModal'
    });

    var _installServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Installs/InstallServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Installs/_InstallServices.js',
        modalClass: 'InstallServicesModel'
    });

    var _dashboardModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Installs/InstallDashboardModal'
    });
    var _utilisationModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Installs/InstallUtilisationModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Installs/_Utilisation.js',
        modalClass: 'InstallUtilisationModal'
    });
    $('#CreateNewInstallButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$installsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _installService.getInstall,
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
                                _installServicesModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('DownloadExcel'),
                            action: function (data) {
                                _installService
                                    .getInstallsToExcel({ id: data.record.id })
                                    .done(function (result) {
                                        app.downloadTempFile(result);
                                    });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteInstall(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "installName"
            },
            {
                targets: 2,
                data: "regionName"
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
    function getInstalls(reload) {
        
        dataTable.ajax.reload();
    }

    function deleteInstall(install) {

        abp.message.confirm(
            "Are you sure you want to delete install?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _installService.deleteInstall({
                        id: install.id
                    }).done(function () {
                        getInstalls(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#InstallFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#InstallFiltersArea').slideUp();
    });
    
    $('#GetInstallsButton, #RefreshInstallsListButton').click(function (e) {
        e.preventDefault();
        getInstalls();
    });
    
})();