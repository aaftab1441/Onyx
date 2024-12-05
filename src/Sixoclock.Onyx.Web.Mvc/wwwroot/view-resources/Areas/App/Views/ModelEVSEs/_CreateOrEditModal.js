(function ($) {
    app.modals.CreateOrEditModelEVSEModal = function () {

        var _modalManager;
        var _modelEVSEService = abp.services.app.modelEVSE;
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

            var modelEVSE = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _modelEVSEService.createOrUpdateModelEVSE(modelEVSE).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };

        $('#ChargepointModelId').change(function () {

            _modelEVSEService.getEVSEIdCount({
                id: $('#ChargepointModelId').val()
            }).done(function (result) {

                $("#EVSEId").val(result);
            })
        });
    };
})(jQuery);