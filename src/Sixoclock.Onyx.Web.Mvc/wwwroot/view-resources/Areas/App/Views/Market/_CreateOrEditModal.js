(function ($) {
    app.modals.CreateOrEditMarketModal = function () {

        var _modalManager;
        var _marketService = abp.services.app.market;
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

            var market = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _marketService.createOrUpdateMarket(market).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);