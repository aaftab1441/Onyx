(function () {
    app.modals.CreateOrEditRuleSetModal = function () {

        var _modalManager;
        var _ruleSetService = abp.services.app.ruleSetService;
        var _$ruleSetForm = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;

            
            _$ruleSetForm = _modalManager.getModal().find('form[name=RuleSetForm]');
            _$ruleSetForm.validate({ ignore: "" });
        };
        
        this.save = function () {
           
            if (!_$ruleSetForm.valid()) {
                return;
            }

            var ruleSet = _$ruleSetForm.serializeFormToObject();
            
            _modalManager.setBusy(true);
            _ruleSetService.createOrUpdateRuleSet({
               
                RuleSet: ruleSet
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditRuleSetModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})();