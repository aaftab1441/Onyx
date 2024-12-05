(function ($) {
    app.modals.CreateOrEditGroupServiceModal = function () {

        var _modalManager;
        var _groupServicesService = abp.services.app.groupService;
        var _groupPriceParamService= abp.services.app.servicePriceParameter;
        var _$groupServicesTable = $('#GroupServicePriceParameterTable');
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
       
var dataTable = _$groupServicesTable.DataTable({
            paging: false,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
               
                _groupPriceParamService.getServicePriceParameters($("#ServiceId").val()).done(function (result) {

                    _$groupServicesTable.data = result.priceParameters;
                    callback(_$groupServicesTable);
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

            var groupService = _$form.serializeFormToObject();
            debugger;
            _modalManager.setBusy(true);

            _groupServicesService.createOrUpdateService(groupService).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditGroupServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);