(function ($) {
    app.modals.EditNetworkAndGSMSettingsModal = function () {

        var _modalManager;
        var _connectorService = abp.services.app.connector;
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

            var connector = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _connectorService.editNetworkAndGSMSettingsModal(connector).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);