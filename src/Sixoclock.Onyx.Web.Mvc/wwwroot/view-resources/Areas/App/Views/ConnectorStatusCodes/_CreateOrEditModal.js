(function ($) {
    app.modals.CreateOrEditConnectorStatusCodeModal = function () {

        var _modalManager;
        var _connectorStatusCodeService = abp.services.app.connectorStatusCode;
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

            var connectorStatusCode = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _connectorStatusCodeService.createOrUpdateConnectorStatusCode(connectorStatusCode).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);