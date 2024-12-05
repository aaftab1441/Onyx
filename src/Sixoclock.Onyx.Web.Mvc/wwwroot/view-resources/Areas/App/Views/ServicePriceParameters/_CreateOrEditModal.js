(function () {
    app.modals.CreateOrEditPriceParameterModal = function () {

        var _modalManager;
        var _priceParameterservice = abp.services.app.servicePriceParameter;
        var _$priceParameterForm = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;


            _$priceParameterForm = _modalManager.getModal().find('form[name=PriceParameterForm]');
            _$priceParameterForm.validate({ ignore: "" });
        };

        this.save = function() {
         
            if (!_$priceParameterForm.valid()) {
                return;
            }
            var priceParameter = _$priceParameterForm.serializeFormToObject();
            _modalManager.setBusy(true);
            _priceParameterservice.createOrUpdateServicePriceParameter({
                PriceParameter: priceParameter
            }).done(function() {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.CreateOrEditPriceParameterModalSaved');
            }).always(function() {
                _modalManager.setBusy(false);

            });
        };
    };
   
})();