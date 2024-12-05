(function ($) {
    app.modals.CreateOrEditRegionServiceModal = function () {
        debugger
        var _modalManager;
        var _regionServicesService = abp.services.app.regionService;
        var _regionPriceParamService = abp.services.app.servicePriceParameter;
        var _$regionServicesTable = $('#RegionServicePriceParameterTable');
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };

        var dataTable = _$regionServicesTable.DataTable({
            paging: false,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {

                _regionPriceParamService.getServicePriceParameters($("#ServiceId").val()).done(function (result) {

                    _$regionServicesTable.data = result.priceParameters;
                    callback(_$regionServicesTable);
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
            var regionService = _$form.serializeFormToObject();
            debugger;
            _modalManager.setBusy(true);

            _regionServicesService.createOrUpdateService(regionService).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditRegionServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);