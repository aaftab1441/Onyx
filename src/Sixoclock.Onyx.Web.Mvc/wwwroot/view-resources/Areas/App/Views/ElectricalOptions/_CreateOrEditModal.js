(function ($) {
    app.modals.CreateOrEditElectricalOptionModal = function () {

        var _modalManager;
        var _electricalOptionService = abp.services.app.electricalOption;
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

            var electricalOption = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _electricalOptionService.createOrUpdateElectricalOption(electricalOption).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);