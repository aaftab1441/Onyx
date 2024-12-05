(function ($) {
    app.modals.CreateOrEditRegionModal = function () {

        var _modalManager;
        var _regionService = abp.services.app.region;
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

            var region = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _regionService.createOrUpdateRegion(region).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);