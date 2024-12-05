(function ($) {
    app.modals.CreateOrEditChargepointModelModal = function () {

        var _modalManager;
        var _chargepointModelService = abp.services.app.chargePointModel;
        var _oCPPFeatureService = abp.services.app.oCPPFeature;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
            
            _$releaseInformationsForm = _modalManager.getModal().find('form[name=ReleaseInformationsForm]');
            _$communicationInformationsForm = _modalManager.getModal().find('form[name=CommunicationInformationsForm]');
            _$electricalInformationsForm = _modalManager.getModal().find('form[name=ElectricalInformationsForm]');
            _$otherInformationsForm = _modalManager.getModal().find('form[name=OtherInformationsForm]');
            _$featuresInformationsForm = _modalManager.getModal().find('form[name=FeaturesInformationsForm]');
        };

        this.save = function () {
            
            if (!_$form.valid()) {
                return;
            }

            var Features = _$featuresInformationsForm.serializeArray();
            var modelFeatures = [];
            for (var i = 0; i < Features.length; i++) {

                modelFeatures.push({
                    OCPPFeatureId: parseInt(Features[i].value.split("_")[0]),
                    FeatureName: Features[i].value.split("_")[1],
                    ChargepointModelId: 0,
                    TenantId: 0
                });
            }
            
            var chargepointModel = _$form.serializeFormToObject();
            chargepointModel.ModelFeatures = modelFeatures;

            _modalManager.setBusy(true);

            _chargepointModelService.createOrUpdateChargepointModel(chargepointModel).done(function (result) {

                var Releases = _$releaseInformationsForm.serializeArray();
                var releases = [];
                for (var i = 0; i < Releases.length; i++) {
                    releases.push({
                        ChargeReleaseOptionId: Releases[i].value,
                        ChargepointModelId: result
                    });
                }

                var Electrics = _$electricalInformationsForm.serializeArray();
                var electrics = [];
                for (var i = 0; i < Electrics.length; i++) {
                    electrics.push({
                        ElectricalOptionId: Electrics[i].value,
                        ChargepointModelId: result
                    });
                }

                var Coms = _$communicationInformationsForm.serializeArray();
                var coms = [];
                for (var i = 0; i < Coms.length; i++) {
                    coms.push({
                        ComOptionId: Coms[i].value,
                        ChargepointModelId: result
                    });
                }

                var Others = _$otherInformationsForm.serializeArray();
                var others = [];
                for (var i = 0; i < Others.length; i++) {
                    others.push({
                        OtherOptionId: Others[i].value,
                        ChargepointModelId: result
                    });
                }
                
                var chargepointModelOptions = {};
                chargepointModelOptions.releaseOptionModels = releases;
                chargepointModelOptions.electricalOptionModels = electrics;
                chargepointModelOptions.comOptionModels = coms;
                chargepointModelOptions.otherOptionModels = others;

                _chargepointModelService.createOrUpdateChargepointModelOptions(chargepointModelOptions).done(function () {
                    
                    _modalManager.close();
                    location.reload();
                }).always(function () {
                    _modalManager.setBusy(false);
                    });
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
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
    };
})(jQuery);