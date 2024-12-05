(function () {
    $(function () {
 
        var _$grantsTable = $('#GrantsTable');
        var _grantService = abp.services.app.ruleSetService;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Grants.Create'),
            edit: abp.auth.hasPermission('Pages.Grants.Edit'),
            'delete': abp.auth.hasPermission('Pages.Grants.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Grants/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Grants/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditRuleSetModal'
        });
        var _editRulesModel = new app.ModalManager({
            viewUrl: abp.appPath + 'App/GrantRules',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/GrantRules/Index.js',
            modalClass: 'EditRulesModel'
        });

        var dataTable = _$grantsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function(data, callback, settings) {
                _grantService.getRuleSets().done(function (result) {
                    _$grantsTable.data = result.ruleSets;
                    callback(_$grantsTable);
                });
            },
            //listAction: {
            //    ajaxFunction: _grantService.getRuleSets,
            //    inputFilter: function () {
            //        return {
            //            permission: $('#PermissionSelectionCombo').val()
            //        };
            //    }
            //},
            
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
                        items: [{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        },{
                            text: app.localize('EditRules'),
                            //visible: function () {
                            //    return _permissions.edit;
                            //},
                            action: function (data) {
                                _editRulesModel.open({ id: data.record.id });
                            }
                        },{
                            text: app.localize('Delete'),
                            visible: function (data) {
                                return !data.record.isStatic && _permissions.delete;
                            },
                            action: function (data) {
                                deleteGrants(data.record);
                            }
                        }]
                    }
                },{
                    targets: 1,
                    data: "name"
                }, {
                    targets: 2,
                    data: "translation"
                }, {
                    targets: 3,
                    data: "ruleCount"
                }
            ]
        });

        function deleteGrants(grant) {
            abp.message.confirm(
                app.localize('GrantDeleteWarningMessage', grant.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _grantService.deleteRuleSet({
                            id: grant.id
                        }).done(function () {
                            getGrants();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewGrantButton').click(function () {
            _createOrEditModal.open();
        });

        $('#RefreshGrantsButton').click(function (e) {
            e.preventDefault();
            getGrants();
        });

        function getGrants() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditRuleSetModalSaved', function () {
            getGrants();
        });

    });
})();