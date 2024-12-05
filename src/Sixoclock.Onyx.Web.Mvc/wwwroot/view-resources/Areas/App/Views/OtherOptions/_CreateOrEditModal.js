(function ($) {
    app.modals.CreateOrEditOtherOptionModal = function () {

        var _modalManager;
        var _otherOptionService = abp.services.app.otherOption;
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

            var otherOption = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _otherOptionService.createOrUpdateOtherOption(otherOption).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);