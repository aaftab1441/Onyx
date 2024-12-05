//var editor;

(function () {
    app.modals.EditRulesModel = function () {
   
        var _modelManager;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        var _$rulesTable = $('#RulesTable');
        var _rulesService = abp.services.app.ruleService;
        //TODO: add Permissions for Rule
        var _permissions = {
            create: abp.auth.hasPermission('Pages.Grants.Create'),
            edit: abp.auth.hasPermission('Pages.Grants.Edit'),
            'delete': abp.auth.hasPermission('Pages.Grants.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/GrantRules/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/GrantRules/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditRuleModal'
        });
        //TODO: get primary keys of the rules
        //editor = new $.fn.dataTable.Editor({
        //    ajax: _rulesService.getRules(setId),
        //    idSrc: 'entityName', 
        //    table: _$rulesTable,
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

        //_$rulesTable.on('click', 'tbody td:not(:first-child)', function (e) {
        //    editor.inline(this);
        //});

        var setId = $("#RuleSetId").val();
        var dataTable = _$rulesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function(data, callback, settings) {
                _rulesService.getRules(setId).done(function (result) {
                    
                    _$rulesTable.data = result.rules;
                    callback(_$rulesTable);
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
                                    
                                    _createOrEditModal.open({ id: data.record.id, ruleSetId: setId });
                                }
                            }, {
                                text: app.localize('Delete'),
                                visible: function(data) {
                                    return !data.record.isStatic && _permissions.delete;
                                },
                                action: function(data) {
                                    deleteRule(data.record);
                                }
                            }
                        ]
                    }
                }, {
                    targets: 1,
                    data: "relationStart"
                }, {
                    targets: 2,
                    data: "entityName"
                }, {
                    targets: 3,
                    data: "propertyName"
                }, {
                    targets: 4,
                    data: "ruleCondition"
                }, {
                    targets: 5,
                    data: "value"
                }, {
                    targets: 6,
                    data: "relationEnd"
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

        function deleteRule(rule) {
            abp.message.confirm(
                app.localize('GrantDeleteWarningMessage', rule.WhatEver),
                function(isConfirmed) {
                    if (isConfirmed) {
                        _rulesService.deleteRule({
                            id: rule.id
                        }).done(function() {
                            getRules();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewRuleButton').click(function() {
            _createOrEditModal.open({ id: 0, ruleSetId: setId });
        });

        $('#RefreshRulesButton').click(function(e) {
            e.preventDefault();
            getRules();
        });

        function getRules() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditRuleModalSaved',
            function() {
                getRules();
            });
    }

})();