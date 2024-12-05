(function ($) {
    app.modals.CreateOrEditMarketServiceModal = function () {
        debugger
        var _modalManager;
        var _marketServicesService = abp.services.app.marketService;
        var _marketPriceParamService = abp.services.app.servicePriceParameter;
        var _$marketServicesTable = $('#MarketServicePriceParameterTable');
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };

        var dataTable = _$marketServicesTable.DataTable({
            paging: false,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {

                _marketPriceParamService.getServicePriceParameters($("#ServiceId").val()).done(function (result) {

                    _$marketServicesTable.data = result.priceParameters;
                    callback(_$marketServicesTable);
                });
            },
            columnDefs: [
                {
                    targets: 0,
                    data: "name"
                }, {
                    targets: 1,
                    data: "value"
                }

            ]

        });
        var selectedService = $("#ServiceId");
        selectedService.on('change',
            function () {

                dataTable.ajax.reload();
            });
        this.save = function () {

            if (!_$form.valid()) {
                return;
            }
            debugger
            var marketService = _$form.serializeFormToObject();
            debugger;
            _modalManager.setBusy(true);

            _marketServicesService.createOrUpdateService(marketService).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditMarketServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);