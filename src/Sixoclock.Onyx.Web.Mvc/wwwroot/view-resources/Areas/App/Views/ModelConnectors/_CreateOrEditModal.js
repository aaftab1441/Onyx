(function ($) {
    app.modals.CreateOrEditModelConnectorModal = function () {

        var _modalManager;
        var _modelConnectorService = abp.services.app.modelConnector;
        var _chargePointModelService = abp.services.app.chargePointModel;
        var _oCPPFeatureService = abp.services.app.oCPPFeature;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();

            _$featuresInformationsForm = _modalManager.getModal().find('form[name=FeaturesInformationsForm]');

            _$featuresInformationsForm.validate();
        };

        this.save = function () {
            
            if (!_$form.valid()) {
                return;
            }

            var modelConnector = _$form.serializeFormToObject();
            
            if (modelConnector.VendorId == "0" || modelConnector.ChargepointModelId == "0" || modelConnector.ConnectorTypeId == "0" || modelConnector.MeterTypeId == "0" || modelConnector.CapacityId == "0" || modelConnector.OCPPTransportId == "0" || modelConnector.OCPPVersionId == "0") {
                sweetAlert("Please select required fields.");
                return;
            }

            var Features = _$featuresInformationsForm.serializeArray();
            var modelFeatures = [];
            for (var i = 0; i < Features.length; i++) {
                
                modelFeatures.push({
                    OCPPFeatureId: parseInt(Features[i].value.split("_")[0]),
                    FeatureName: Features[i].value.split("_")[1],
                    ConnectorId: 0,
                    TenantId: 0
                });
            }

            modelConnector.ModelFeatures = modelFeatures;

            _modalManager.setBusy(true);

            _modelConnectorService.createOrUpdateModelConnector(modelConnector).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };

        $('#ChargepointModelId').change(function () {

            _modelConnectorService.getConnectorNo({
                id: $('#ChargepointModelId').val()
            }).done(function (data) {

                $('#ConnectorNumber').val(data);
            })
        });

        $('#VendorId').change(function () {

            _chargePointModelService.getChargepointModelsByVendorList({
                id: $('#VendorId').val()
            }).done(function (data) {

                var items = '<option value="0">' + app.localize("DropdownDefault") + '</option>';

                $.each(data.chargepointModels, function () {

                    items += "<option value='" + this.id + "'>" + this.name + "</option>";
                });
                $('#ChargepointModelId').html(items);
            })
        });

        $('#OCPPVersionId').change(function () {
            
            _oCPPFeatureService.getOCPPFeaturesByOCPPVersionList({
                id: $('#OCPPVersionId').val()
            }).done(function (data) {
                
                var items = '';
                if (data.ocppFeatures.length > 0) {
                    $("#OCPPFeaturesForm").css("display", "block");;
                }
                else {
                    $("#OCPPFeaturesForm").css("display", "none");;
                }

                $.each(data.ocppFeatures, function () {
                    
                    items += "<tr><td><label class='control-label col-sm-12 pull-left'><input name='ModelFeatureId' value='" + this.id + " _" + this.name + "' type='checkbox' ) /> " + this.name + "</label></td></tr>";
                });
                $('#OCPPFeatures').html(items);
            })
        });

        $('#ModelEVSEId').change(function () {

            _modelConnectorService.getConnectorNo({
                id: $('#ChargepointModelId').val()
            }).done(function (result) {

                $("#ConnectorId").val(result);
            })
        });
    };
})(jQuery);