(function () {

    var _$regionsTable = $('#RegionsTable');
    var _$filterForm = $('#RegionFilterForm');
    var _regionService = abp.services.app.region;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Regions/CreateOrEditRegionModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Regions/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditRegionModal'
    });

    var _regionServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Regions/RegionServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Regions/_RegionServices.js',
        modalClass: 'RegionServicesModel'
    });

    var _dashboardModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Regions/RegionDashboardModal'
    });
    var _utilisationModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Regions/RegionUtilisationModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Regions/_Utilisation.js',
        modalClass: 'RegionUtilisationModal'
    });

    var dataTable = _$regionsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _regionService.getRegion,
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
                                _regionServicesModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('DownloadExcel'),
                            action: function (data) {
                                _regionService
                                    .getRegionsToExcel({ id: data.record.id })
                                    .done(function (result) {
                                        app.downloadTempFile(result);
                                    });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteRegion(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "regionName"
            },
            {
                targets: 2,
                data: "marketName"
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
    function getRegions(reload) {

        dataTable.ajax.reload();
    }

    function deleteRegion(region) {

        abp.message.confirm(
            "Are you sure you want to delete region?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _regionService.deleteRegion({
                        id: region.id
                    }).done(function () {
                        getRegions(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#RegionFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#RegionFiltersArea').slideUp();
    });

    $('#CreateNewRegionButton').click(function () {
        _createOrEditModal.open();
    });

    $('#GetRegionsButton, #RefreshRegionListButton').click(function (e) {
        e.preventDefault();
        getRegions();
    });
    
})();