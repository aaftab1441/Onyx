(function ($) {
    app.modals.CreateOrEditInstallServiceModal = function () {

        var _modalManager;
        var _installServicesService = abp.services.app.installService;
        var _installPriceParamService= abp.services.app.servicePriceParameter;
        var _$installServicesTable = $('#InstallServicePriceParameterTable');
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
       
var dataTable = _$installServicesTable.DataTable({
            paging: false,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
               
                _installPriceParamService.getServicePriceParameters($("#ServiceId").val()).done(function (result) {

                    _$installServicesTable.data = result.priceParameters;
                    callback(_$installServicesTable);
                });
            },
            columnDefs: [
                 {
                    targets: 0,
                    data: "name"
                }, {
                    targets: 1,
                    data: "value"
                }
               
            ]
           
        });
var selectedService = $("#ServiceId");
        selectedService.on('change',
            function() {
                
                dataTable.ajax.reload();
            });
        this.save = function () {
            
            if (!_$form.valid()) {
                return;
            }

            var installService = _$form.serializeFormToObject();
            debugger;
            _modalManager.setBusy(true);

            _installServicesService.createOrUpdateService(installService).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditInstallServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);