(function ($) {
    app.modals.ViewDetailsTagModal = function () {

        var _modalManager;
        var _tagService = abp.services.app.tag;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
    };
})(jQuery);