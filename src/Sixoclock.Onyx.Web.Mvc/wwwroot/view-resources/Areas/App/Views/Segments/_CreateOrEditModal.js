(function ($) {
    app.modals.CreateOrEditSegmentModal = function () {

        var _modalManager;
        var _segmentService = abp.services.app.segment;
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

            var segment = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _segmentService.createOrUpdateSegment(segment).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);