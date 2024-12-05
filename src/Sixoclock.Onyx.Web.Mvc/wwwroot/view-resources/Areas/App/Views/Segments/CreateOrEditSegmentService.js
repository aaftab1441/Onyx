(function ($) {
    app.modals.CreateOrEditSegmentServiceModal = function () {

        var _modalManager;
        var _segmentServicesService = abp.services.app.segmentService;
        var _segmentPriceParamService= abp.services.app.servicePriceParameter;
        var _$segmentServicesTable = $('#SegmentServicePriceParameterTable');
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
       
var dataTable = _$segmentServicesTable.DataTable({
            paging: false,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
               
                _segmentPriceParamService.getServicePriceParameters($("#ServiceId").val()).done(function (result) {

                    _$segmentServicesTable.data = result.priceParameters;
                    callback(_$segmentServicesTable);
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

            var segmentService = _$form.serializeFormToObject();
            debugger;
            _modalManager.setBusy(true);

            _segmentServicesService.createOrUpdateService(segmentService).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditSegmentServiceModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);