(function ($) {
    app.modals.CreateOrEditConnectorModal = function () {

        var _modalManager;
        var _connectorService = abp.services.app.connector;
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

            var connector = _$form.serializeFormToObject();
            
            var Features = _$featuresInformationsForm.serializeArray();
            var connectorFeatures = [];
            for (var i = 0; i < Features.length; i++) {

                connectorFeatures.push({
                    OCPPFeatureId: parseInt(Features[i].value.split("_")[0]),
                    FeatureName: Features[i].value.split("_")[1],
                    ChargepointModelConnectorId: 0,
                    TenantId: 0
                });
            }

            connector.ConnectorFeatures = connectorFeatures;

            _modalManager.setBusy(true);

            _connectorService.createOrUpdateConnector(connector).done(function () {
                _modalManager.close();
                location.reload();
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