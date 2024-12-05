(function ($) {
    app.modals.CreateOrEditVendorModal = function () {

        var _modalManager;
        var _vendorService = abp.services.app.vendor;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };

        this.save = function () {

            if (!_$form.valid()) {
                return;
            }

            var vendor = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _vendorService.createOrUpdateVendor(vendor).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);