(function ($) {
    app.modals.CreateOrEditCapacityModal = function () {

        var _modalManager;
        var _capacityService = abp.services.app.capacity;
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

            var capacity = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _capacityService.createOrUpdateCapacity(capacity).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);