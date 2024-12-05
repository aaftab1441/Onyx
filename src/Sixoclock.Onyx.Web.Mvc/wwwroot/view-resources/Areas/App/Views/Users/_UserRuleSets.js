(function ($) {
    app.modals.UserRulesSetModal = function () {

        var _$userRuleSetsTable = $('#UserRuleSetsTable');
        var _userRuleSetService = abp.services.app.userRuleSetService;

        //TODO: Add Permissions
        var _permissions = {
            //create: abp.auth.hasPermission('Pages.Administration.Users.Create'),
            //edit: abp.auth.hasPermission('Pages.Administration.Users.Edit'),
            //changePermissions: abp.auth.hasPermission('Pages.Administration.Users.ChangePermissions'),
            //impersonation: abp.auth.hasPermission('Pages.Administration.Users.Impersonation'),
            'delete': abp.auth.hasPermission('Pages.RuleSets.Delete')
        };

        $(function () {
            //var selectedResultSet;
            var ruleSetResult;

            var ruleSets = new Bloodhound({
                datumTokenizer: function (datum) {
                    return Bloodhound.tokenizers.whitespace(datum);
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url: abp.appPath + 'api/services/app/UserRuleSetService/GetRuleSetByName?name=%QUERY',
                    wildcard: '%QUERY',
                    cache: false,
                    transform: function (response) {
                        //ruleSetResult = response.result;
                        return $.map(response.result.ruleSets,
                            function (ruleset) {
                                return {
                                    id: ruleset.id,
                                    value: ruleset.name
                                };
                            });
                    }
                }
            });

            ruleSets.clearPrefetchCache();
            ruleSets.initialize();

            $('#ResultSet').typeahead(
                {
                    hint: true,
                    highlight: true,
                    minLength: 3,
                    limit: Infinity
                },
                {
                    displayKey: 'value',
                    valueKey: 'value',
                    source: ruleSets,
                    limit: 5
                }).on('typeahead:selected', function (event, data) {
                    selectedResultSet = data.id;
                    //$('#ResultSet').val(data.id);
                    $("#AddUserRuleSetButton").prop('disabled', false);
                });

            $("#ResultSet").on('input propertychange paste', function () {
                $("#AddUserRuleSetButton").prop('disabled', true);
            });
            $("#AddUserRuleSetButton").on('click', function () {
                var val = $('#ResultSet').val();
                //var index = ruleSetResult.ruleSets.map(function (e) { return e.name; }).indexOf(val);
                //if (index >= 0) {
                    _userRuleSetService.setUserRuleSet({
                        userId: setId,
                        ruleSetId: selectedResultSet
                    });
                    $('#ResultSet').val("");
                    setTimeout(function () {
                        getUserRulesSets();
                    }, 500);
                //}
                
            });
        });
        var setId = $("#RuleSetId").val();
        var dataTable = _$userRuleSetsTable .DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function (data, callback, settings) {
                _userRuleSetService.getUserRuleSets({
                    id: setId
                }).done(function (result) {
                    _$userRuleSetsTable.data = result.userRuleSets;
                    callback(_$userRuleSetsTable);
                });
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
                        items: [{
                            text: app.localize('Delete'),
                            visible: function () {
                                debugger;
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteuserRuleSet(data.record);
                            }
                        }]
                    }
                }, {
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
        
        function deleteuserRuleSet(userRuleSet) {
            abp.message.confirm(
                app.localize('GrantDeleteWarningMessage', rule.WhatEver),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _userRuleSetService.deleteUserRuleSet({
                            id: userRuleSet.id
                        }).done(function () {
                            getUserRulesSets();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        function getUserRulesSets() {
            dataTable.ajax.reload();
        }
    }
})(jQuery);