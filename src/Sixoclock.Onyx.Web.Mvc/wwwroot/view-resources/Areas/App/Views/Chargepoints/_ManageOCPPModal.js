(function ($) {
    app.modals.ManageOCPPModal = function () {

        var _modalManager;
        var _$oCPPTable = $('#OCPPTable');
        var _chargepointService = abp.services.app.chargepoint;
        var _chargepointKeyValueService = abp.services.app.chargepointKeyValue;
        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ChargepointOCPPConfigs/EditChargepointKeyValueModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ChargepointOCPPConfigs/_EditModal.js',
            modalClass: 'EditChargepointKeyValuesModal'
        });
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        
        var dataTable = _$oCPPTable.DataTable({

            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _chargepointKeyValueService.getChargepointKeyValuesListByChargepoint,
                inputFilter: function () {
                    return { chargepointId: $("#Id").val() }
                }
            },

            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-xs btn-primary blue',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('EditSpineValue'),
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.id });
                                }
                            }, {
                                text: app.localize('GetFromCP'),
                                action: function (data) {
                                    getFromCP(data.record.id);
                                }
                            }, {
                                text: app.localize('SetValueInCP'),
                                action: function (data) {
                                    setValueInCP(data.record.id);
                                }
                            }
                        ]
                    }

            fields: {
                id: {
                    key: true,
                    list: false
                },
                actions: {
                    title: app.localize('Actions'),
                    width: '7%',
                    sorting: false,
                    type: 'record-actions',
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [{
                        text: app.localize('EditSpineValue'),
                        action: function (data) {
                            
                        }
                    }, {
                        text: app.localize('GetFromCP'),
                        action: function (data) {
                            getFromCP(data.record.id);
                        }
                    }, {
                        text: app.localize('SetValueInCP'),
                        action: function (data) {
                            setValueInCP(data.record.id);
                        }
                    }]
                },
                featureName: {
                    title: app.localize('JustFeature'),
                    width: '10%'
                },
                key: {
                    title: app.localize('Key'),
                    width: '7%'
                },
                {
                    targets: 1,
                    data: "featureName"
                },
                {
                    targets: 2,
                    data: "key"
                },
                {
                    targets: 3,
                    data: "chargepointValue"
                },
                {
                    targets: 4,
                    data: "wildValue"
                },
                {
                    targets: 5,
                    data: "rw"
                }
            ]
        });
        function getCharegpointKeys() {

            dataTable.ajax.reload();
        }
        abp.event.on('app.editChargepointKeyValuesModalSaved', function () {
            
            getCharegpointKeys();
        });
        function getFromCP(id) {

            var keyValue = new Object();
            keyValue.chargepointId = $("#Id").val();
            keyValue.chargepointKeyValueId = id;
            _chargepointService.getChargepointKeyValueFromCP(keyValue).done(function (data) {

                sweetAlert("Get from cp command for this key command is sent");

            });
        }
        function setValueInCP(id) {

            var keyValue = new Object();
            keyValue.chargepointId = $("#Id").val();
            keyValue.chargepointKeyValueId = id;
            _chargepointService.setChargepointKeyValueInCP(keyValue).done(function (data) {

                sweetAlert("Set value in cp command for this key is sent");

            });
        }

        $('#editSpineModel').on('hidden', function () {
            // do something…
            debugger
        })
    };
})(jQuery);

function RefreshValuesFromCP() {

    var _chargepointService = abp.services.app.chargepoint;

    _chargepointService.refreshChargepointKeyValuesFromCP({ id: $("#Id").val() }).done(function (data) {

        sweetAlert("Get conficguration command for refersh values from CP is sent");

    });
}
function setAllValues() {

    var _chargepointService = abp.services.app.chargepoint;

    _chargepointService.setConfigurationAllCPKeyValues({ id: $("#Id").val() }).done(function (data) {

        sweetAlert("Set all values conficguration command is sent");

    });
}