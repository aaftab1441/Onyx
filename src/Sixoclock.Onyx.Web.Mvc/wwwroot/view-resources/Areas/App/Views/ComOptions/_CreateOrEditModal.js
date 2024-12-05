(function ($) {
    app.modals.CreateOrEditComOptionModal = function () {

        var _modalManager;
        var _comOptionService = abp.services.app.comOption;
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

            var comOption = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _comOptionService.createOrUpdateComOption(comOption).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);