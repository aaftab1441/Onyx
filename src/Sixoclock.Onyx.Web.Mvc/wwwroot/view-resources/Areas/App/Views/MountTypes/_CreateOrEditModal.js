(function ($) {
    app.modals.CreateOrEditMountTypeModal = function () {

        var _modalManager;
        var _mountTypeService = abp.services.app.mountType;
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

            var mountType = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _mountTypeService.createOrUpdateMountType(mountType).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);