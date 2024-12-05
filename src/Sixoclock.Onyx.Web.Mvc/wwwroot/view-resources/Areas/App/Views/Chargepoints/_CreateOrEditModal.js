(function ($) {
    app.modals.CreateOrEditChargepointModal = function () {

        var _modalManager;
        var _chargepointService = abp.services.app.chargepoint;
        var _chargePointModelService = abp.services.app.chargePointModel;
        var _oCPPFeatureService = abp.services.app.oCPPFeature;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();

            _$featuresInformationsForm = _modalManager.getModal().find('form[name=FeaturesInformationsForm]');
        };

        this.save = function () {

            if (!_$form.valid()) {
                return;
            }

            var Features = _$featuresInformationsForm.serializeArray();
            var chargepointFeatures = [];
            for (var i = 0; i < Features.length; i++) {

                chargepointFeatures.push({
                    OCPPFeatureId: parseInt(Features[i].value.split("_")[0]),
                    FeatureName: Features[i].value.split("_")[1],
                    ChargepointId: 0,
                    TenantId: 0
                });
            }

            var chargepoint = _$form.serializeFormToObject();
            chargepoint.ChargepointFeature = chargepointFeatures;

            _modalManager.setBusy(true);

            _chargepointService.createOrUpdateChargepoint(chargepoint).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };

        $('#MountTypeId').change(function () {
            
            _chargePointModelService.getChargepointModelsByMountList({
                id: $('#MountTypeId').val()
            }).done(function (data) {

                var items = '<option value="0">' + app.localize("DropdownDefault") + '</option>';

                $.each(data.chargepointModels, function () {

                    items += "<option value='" + this.id + "'>" + this.name + "</option>";
                });
                $('#ChargepointModelId').html(items);
            })
        });

        $('#ChargepointModelId').change(function () {

            _chargePointModelService.getChargepointModelForChargepoint({
                id: $('#ChargepointModelId').val()
            }).done(function (data) {
                
                if (data != null) {
                    $("#lblModel").text("Vendor : " + data.vendor + "");
                    $("#lblConnectors").text("Connectors : " + data.noOfConnectors + "");
                    $("#Connectors").val(data.noOfConnectors);
                    $("#OCPPVersionId").val(data.ocppVersionId);
                    $("#OCPPTransportId").val(data.ocppTransportId);
                    $("#lblCapacity").text("Capacity : " + data.capacity + "");
                }
                else {
                    $("#lblModel").text("Vendor : ");
                    $("#lblConnectors").text("Connectors : ");
                    $("#Connectors").val();
                    $("#OCPPVersionId").val();
                    $("#OCPPTransportId").val();
                    $("#lblCapacity").text("Capacity : ");
                }
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

                    items += "<tr><td><label class='control-label col-sm-12 pull-left'><input name='ChargepointFeatureId' value='" + this.id + " _" + this.name + "' type='checkbox' ) /> " + this.name + "</label></td></tr>";
                });
                $('#OCPPFeatures').html(items);
            })
        });
    };
})(jQuery);