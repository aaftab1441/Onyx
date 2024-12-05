(function () {

    var _$configsTable = $('#ConfigsTable');
    var _$filterForm = $('#CapacityFilterForm');
    var _configService = abp.services.app.config;
    var _featureService = abp.services.app.oCPPFeature
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Configs/CreateOrEditConfigModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Configs/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditConfigModal'
    });

    $('#CreateNewConfigButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$configsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _configService.getConfig,
            inputFilter: function () {
                return createRequestParams();
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
                            text: app.localize('Edit'),
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Delete'),
                            visible: function (data) {
                                return false;
                            },
                            action: function (data) {
                                deleteConfig(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "versionName"
            },
            {
                targets: 2,
                data: "featureName"
            },
            {
                targets: 3,
                data: "key"
            },
            {
                targets: 4,
                data: "defaultValue"
            },
            {
                targets: 5,
                data: "rw"
            },
            {
                targets: 6,
                data: "comment",
                render: function (data, type, full, meta) {

                    return "<div style='white-space:normal;width:100%'>" + data + "</div>";
                }
            },
            {
                targets: 7,
                data: "creationTime",
                render: function (creationTime) {
                    return moment(creationTime).format('L');
                }
            }
        ]
    });
    function createRequestParams() {
        var prms = {};
        _$filterForm.serializeArray().map(function (x) { prms[abp.utils.toCamelCase(x.name)] = x.value; });
        return prms;
    }
    function getConfigs() {
        
        dataTable.ajax.reload();
    }

    function deleteConfig(config) {

        abp.message.confirm(
            "Are you sure you want to delete key value?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _configService.deleteConfig({
                        id: config.id
                    }).done(function () {
                        getConfigs(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#ConfigFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#ConfigFiltersArea').slideUp();
    });

    $('#GetConfigsButton, #RefreshConfigsListButton').click(function (e) {
        e.preventDefault();
        getConfigs();
    });

    $('#versionId').change(function () {

        _featureService.getOCPPFeaturesListByVersionId({
            id: $('#versionId').val()
        }).done(function (data) {
            
            var items = '<option value="0">' + app.localize("SelectFeature") + '</option>';

            $.each(data.ocppFeatures, function () {
                
                items += "<option value='" + this.id + "'>" + this.name + "</option>";
            });
            $('#featureId').html(items);
        })
    });

})();