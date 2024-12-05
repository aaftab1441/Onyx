(function ($) {
    app.modals.OverViewModal = function () {
        
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
    };

})(jQuery);