(function () {
    app.modals.CreateOrEditServiceModal = function () {
      
        var _modalManager;
        var _servicesService = abp.services.app.services;
        var _servicesFrom = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;

            
            _servicesFrom = _modalManager.getModal().find('form[name=ServiceForm]');
            _servicesFrom.validate({ ignore: "" });
        };
        
        this.save = function () {
           
            if (!_servicesFrom.valid()) {
                return;
            }

            var service = _servicesFrom.serializeFormToObject();
           
            _modalManager.setBusy(true);
            _servicesService.createOrUpdateService({
                
                Service: service
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.CreateOrEditServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})();