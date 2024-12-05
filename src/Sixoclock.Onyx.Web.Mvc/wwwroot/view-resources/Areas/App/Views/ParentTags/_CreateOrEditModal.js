(function ($) {
    app.modals.CreateOrEditParentTagModal = function () {

        var _modalManager;
        var _parentTagService = abp.services.app.parentTag;
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

            var parentTag = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _parentTagService.createOrUpdateParentTag(parentTag).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);