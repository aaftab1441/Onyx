(function ($) {
    app.modals.CreateOrEditInstallModal = function () {

        var _modalManager;
        var _installService = abp.services.app.install;
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

            var install = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _installService.createOrUpdateInstall(install).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);