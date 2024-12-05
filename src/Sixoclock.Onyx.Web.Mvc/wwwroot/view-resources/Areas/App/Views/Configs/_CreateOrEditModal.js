(function ($) {
    app.modals.CreateOrEditConfigModal = function () {

        var _modalManager;
        var _configService = abp.services.app.config;
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

            var config = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _configService.createOrUpdateConfig(config).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);

function setDefaultValueTrue() {
    
    $("input[name=DefaultValue]").val("TRUE");
}
function setDefaultValueFalse() {
    
    $("input[name=DefaultValue]").val("FALSE");
}