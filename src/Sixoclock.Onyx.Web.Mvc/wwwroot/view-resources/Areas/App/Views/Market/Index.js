(function () {

    var _$marketsTable = $('#MarketsTable');
    var _$filterForm = $('#MarketFilterForm');
    var _marketService = abp.services.app.market;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Markets/CreateOrEditMarketModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Market/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditMarketModal'
    });

    var _marketServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Markets/MarketServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Market/_MarketServices.js',
        modalClass: 'MarketServicesModel'
    });

    var _dashboardModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Markets/MarketDashboardModal'
    });
    $('#CreateNewMarketButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    var _utilisationModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Markets/MarketUtilisationModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Market/_Utilisation.js',
        modalClass: 'MarketUtilisationModal'
    });

    var dataTable = _$marketsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _marketService.getMarket,
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
                                _marketServicesModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('DownloadExcel'),
                            action: function (data) {
                                _marketService
                                    .getMarketsToExcel({ id: data.record.id })
                                    .done(function (result) {
                                        app.downloadTempFile(result);
                                    });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteMarket(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "marketName"
            },
            {
                targets: 2,
                data: "customerName"
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
    function getMarkets() {
        
        dataTable.ajax.reload();
    }

    function deleteMarket(market) {

        abp.message.confirm(
            "Are you sure you want to delete the market?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _marketService.deleteMarket({
                        id: market.id
                    }).done(function () {
                        getMarkets(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#MarketFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#MarketFiltersArea').slideUp();
    });
    
    $('#GetMarketsButton, #RefreshMarketListButton').click(function (e) {
        e.preventDefault();
        getMarkets();
    });

})();