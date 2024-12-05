(function ($) {
    app.modals.CreateOrEditAdminStatusModal = function () {

        var _modalManager;
        var _adminStatusService = abp.services.app.adminStatus;
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

            var adminStatus = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _adminStatusService.createOrUpdateAdminStatus(adminStatus).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);