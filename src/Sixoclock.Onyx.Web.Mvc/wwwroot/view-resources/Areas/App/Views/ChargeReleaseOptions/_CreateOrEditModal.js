(function ($) {
    app.modals.CreateOrEditChargeReleaseOptionModal = function () {

        var _modalManager;
        var _chargeReleaseOptionService = abp.services.app.chargeReleaseOption;
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

            var chargeReleaseOption = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _chargeReleaseOptionService.createOrUpdateChargeReleaseOption(chargeReleaseOption).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);