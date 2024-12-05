(function () {

    var _$keyCardsTable = $('#KeyCardsTable');
    var _tagService = abp.services.app.tag;
    
    var dataTable = _$keyCardsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _tagService.getKeyCards
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
                            text: app.localize('ActivateKey'),
                            action: function (data) {

                                abp.message.confirm(
                                    "Are you sure you want to activate tag?",
                                    function (isConfirmed) {
                                        if (isConfirmed) {
                                            _tagService.activateTag({
                                                id: data.record.id
                                            }).done(function () {
                                                getKeyCards();
                                                abp.notify.success(app.localize('SuccessfullyActivated'));
                                            });
                                        }
                                    });
                            }
                        }, {
                            text: app.localize('InactivateKey'),
                            action: function (data) {

                                abp.message.confirm(
                                    "Are you sure you want to inactivate tag?",
                                    function (isConfirmed) {
                                        if (isConfirmed) {
                                            _tagService.inActivateTag({
                                                id: data.record.id
                                            }).done(function () {
                                                getKeyCards();
                                                abp.notify.success(app.localize('SuccessfullyInActivated'));
                                            });
                                        }
                                    });
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "idToken"
            },
            {
                targets: 2,
                data: "authorizationStatus"
            },
            {
                targets: 3,
                data: "kwhCharged"
            },
            {
                targets: 4,
                data: "transactions"
            },
            {
                targets: 5,
                data: "expiry",
                render: function (expiry) {
                    return moment(expiry).format('L');
                }
            },
            {
                targets: 6,
                data: "lastUsed",
                render: function (lastUsed) {
                    return moment(lastUsed).format('L');
                }
            }
        ]
    });

    function getKeyCards() {

        dataTable.ajax.reload();
    }

})();