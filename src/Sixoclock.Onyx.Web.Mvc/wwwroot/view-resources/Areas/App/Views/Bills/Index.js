(function () {
    $(function () {

        var $billsTable = $('#BillsTable');
        var billService = abp.services.app.bill;

        var _permissions = {
            'delete': abp.auth.hasPermission('Pages.Grants.Delete')
        };
        var _addCommentModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Bills/GetBillToAddComment',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Bills/_AddCommentModal.js',
            modalClass: 'AddCommentModal'
        });
        var dataTable = $billsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            ajax: function(data, callback, settings) {
                billService.getBillsList().done(function (result) {
                  
                    $billsTable.data = result.bills;
                    callback($billsTable);
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
                            text: app.localize('AddComment'),
                            //visible: function () {
                            //    return _permissions.addComment;
                            //},
                            action: function (data) {
                                _addCommentModal.open({ id: data.record.id });
                            }
                        },{
                            text: app.localize('Print'),
                            //visible: function (data) {
                            //    return !data.record.isStatic && _permissions.delete;
                            //},
                            action: function (data) {
                                print(data.record);
                            }
                        }]
                    }
                },{
                    targets: 1,
                    data: "number"
                }, {
                    targets: 2,
                    data: "billStatus"
                }, {
                    targets: 3,
                    data: "billType"
                }
                , {
                    targets: 4,
                    data: "totalkwh"
                }
                , {
                    targets: 5,
                    data: "transactions"
                }
                , {
                    targets: 6,
                    data: "comment"
                }
                , {
                    targets: 7,
                    data: "billDate"
                }
                , {
                    targets: 8,
                    data: "dueDate"
                }
            ]
        });

        //function deleteGrants(grant) {
        //    abp.message.confirm(
        //        app.localize('GrantDeleteWarningMessage', grant.WhatEver),
        //        function (isConfirmed) {
        //            if (isConfirmed) {
        //                _grantService.deleteRuleSet({
        //                    id: grant.id
        //                }).done(function () {
        //                    getGrants();
        //                    abp.notify.success(app.localize('SuccessfullyDeleted'));
        //                });
        //            }
        //        }
        //    );
        //};

        //$('#CreateNewGrantButton').click(function () {
        //    _createOrEditModal.open();
        //});

        $('#ExportUsersToExcelButton').click(function () {
            exportToExcel();
        });

        
        $('#RefreshGrantsButton').click(function (e) {
            e.preventDefault();
            getBills();
        });

        function getBills() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditBillModalSaved', function () {
            getBills();
        });
        function exportToExcel() {
            billService
                .getBillsToExcel({})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        };

    });
})();