(function ($) {
    app.modals.CreateOrEditGroupModal = function () {

        var _modalManager;
        var _groupService = abp.services.app.group;
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

            var group = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _groupService.createOrUpdateGroup(group).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);