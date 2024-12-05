(function ($) {
    app.modals.EditChargepointKeyValuesModal = function () {

        var _modalManager;
        var _chargepointKeyValuesService = abp.services.app.chargepointKeyValue;
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
            debugger;
            var chargepointKeyValues = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _chargepointKeyValuesService.updateChargepointKeyValue(chargepointKeyValues).done(function () {
                _modalManager.close();
                abp.event.trigger('app.editChargepointKeyValuesModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);