﻿(function ($) {
    app.modals.EditModelKeyValuesModal = function () {

        var _modalManager;
        var _modelKeyValuesService = abp.services.app.modelKeyValue;
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

            var modelKeyValues = _$form.serializeFormToObject();
            
            _modalManager.setBusy(true);

            _modelKeyValuesService.updateModelKeyValue(modelKeyValues).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);