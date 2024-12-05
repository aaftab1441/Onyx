(function ($) {
    app.modals.CreateOrEditCustomerServiceModal = function () {
        debugger
        var _modalManager;
        var _customerServicesService = abp.services.app.customerService;
        var _customerPriceParamService = abp.services.app.servicePriceParameter;
        var _$customerServicesTable = $('#CustomerServicePriceParameterTable');
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };

        var dataTable = _$customerServicesTable.DataTable({
            paging: false,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {

                _customerPriceParamService.getServicePriceParameters($("#ServiceId").val()).done(function (result) {

                    _$customerServicesTable.data = result.priceParameters;
                    callback(_$customerServicesTable);
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

            var customerService = _$form.serializeFormToObject();
            debugger;
            _modalManager.setBusy(true);

            _customerServicesService.createOrUpdateService(customerService).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditCustomerServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);