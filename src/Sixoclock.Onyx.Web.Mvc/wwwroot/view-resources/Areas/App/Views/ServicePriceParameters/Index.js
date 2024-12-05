//var editor;

(function () {
    app.modals.EditPriceParameterModel = function () {
        
        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$priceParameterTable = $('#PriceParametersTable');
        var _priceParameterService = abp.services.app.servicePriceParameter;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Services.Create'),
            edit: abp.auth.hasPermission('Pages.Services.Edit'),
            'delete': abp.auth.hasPermission('Pages.Services.Delete')
        };
        
        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ServicePriceParameters/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ServicePriceParameters/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditPriceParameterModal'
        });
        //TODO: get primary keys of the rules
        //editor = new $.fn.dataTable.Editor({
        //    ajax: _priceParameterService.getRules(setId),
        //    idSrc: 'entityName', 
        //    table: _$priceParameterTable,
        //    fields: [{
        //        label: "Relation Start",
        //        name: "relationStart"
        //    }, {
        //            label: "Entity Name",
        //            name: "entityName"
        //    }, {
        //            label: "Property Name",
        //            name: "propertyName"
        //    }, {
        //            label: "Rule Condition",
        //            name: "ruleCondition"
        //    }, {
        //            label: "Relation End",
        //            name: "relationEnd.displayText",
        //            type: "select"
        //    }]
        //});

        //_$priceParameterTable.on('click', 'tbody td:not(:first-child)', function (e) {
        //    editor.inline(this);
        //});

        var serviceId = $("#ServiceId").val();
        var dataTable = _$priceParameterTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function(data, callback, settings) {
             _priceParameterService.getServicePriceParameters(serviceId).done(function (result) {
                    
                    _$priceParameterTable.data = result.priceParameters;
                    callback(_$priceParameterTable);
                });
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: true,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-xs btn-primary blue',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('Edit'),
                                visible: function() {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    
                                    _createOrEditModal.open({ id: data.record.id, serviceId: serviceId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function(data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function(data) {
                                    deletePriceParameter(data.record);
                                }
                            }
                        ]
                    }
                }, {
                    targets: 1,
                    data: "name"
                }, {
                    targets: 2,
                    data: "value"
                }
            ]
            //,
            //select: {
            //    style: 'os',
            //    selector: 'td:first-child'
            //},
            //buttons: [
            //    //{ extend: "create", editor: editor },
            //    //{ extend: "edit", editor: editor },
            //    //{ extend: "remove", editor: editor }
            //]
        });

        function deletePriceParameter(priceParameter) {
            abp.message.confirm(
                app.localize('PriceParameteDeleteWarningMessage', priceParameter.WhatEver),
                function(isConfirmed) {
                    if (isConfirmed) {
                        _priceParameterService.deleteServicePriceParameter({
                            id: priceParameter.id
                        }).done(function() {
                            getServicePriceParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewPriceParameterButton').click(function () {
            
            _createOrEditModal.open({ id: 0, serviceId: serviceId });
        });

        $('#RefreshPriceParametersButton').click(function(e) {
            e.preventDefault();
            getServicePriceParameters();
        });

        function getServicePriceParameters() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.CreateOrEditPriceParameterModalSaved',
            function() {
                getServicePriceParameters();
            });
    }

})();