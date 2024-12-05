(function () {
    app.modals.AddCommentModal = function () {

        var _modalManager;
        var billService = abp.services.app.bill;
        var billForm = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;

            
            billForm = _modalManager.getModal().find('form[name=AddCommentForm]');
            billForm.validate({ ignore: "" });
        };
        
        this.save = function () {
           
            if (!billForm.valid()) {
                return;
            }

            var bill = billForm.serializeFormToObject();
            
            _modalManager.setBusy(true);
            billService.addComment({
                Id: bill.Id,
                Comment:bill.Comment
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditBillModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})();