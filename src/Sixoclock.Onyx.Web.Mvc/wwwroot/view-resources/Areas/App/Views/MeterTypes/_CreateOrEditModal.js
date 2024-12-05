(function ($) {
    app.modals.CreateOrEditMeterTypeModal = function () {

        var _modalManager;
        var _meterTypeService = abp.services.app.meterType;
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

            var meterType = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _meterTypeService.createOrUpdateMeterType(meterType).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);