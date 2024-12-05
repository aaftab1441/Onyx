(function ($) {
    app.modals.CreateOrEditVendorErrorCodeModal = function () {

        var _modalManager;
        var _vendorErrorCodeService = abp.services.app.vendorErrorCode;
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

            var vendorErrorCode = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _vendorErrorCodeService.createOrUpdateVendorErrorCode(vendorErrorCode).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);