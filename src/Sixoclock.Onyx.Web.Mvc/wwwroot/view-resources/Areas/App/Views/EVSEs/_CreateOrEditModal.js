(function ($) {
    app.modals.CreateOrEditEVSEModal = function () {

        var _modalManager;
        var _eVSEService = abp.services.app.eVSE;
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

            var eVSE = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _eVSEService.createOrUpdateEVSE(eVSE).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };

        $('#ChargepointId').change(function () {

            _eVSEService.getEVSEIdCount({
                id: $('#ChargepointId').val()
            }).done(function (result) {

                $("#EVSEId").val(result);
            })
        });
    };
})(jQuery);