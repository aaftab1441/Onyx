(function () {

    var _$capacitiesTable = $('#CapacitiesTable');
    var _$filterForm = $('#CapacityFilterForm');
    var _capacitieservice = abp.services.app.capacity;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Capacities/CreateOrEditCapacityModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Capacities/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditCapacityModal'
    });
    
    $('#CreateNewCapacityButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });

    var dataTable = _$capacitiesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _capacitieservice.getCapacity,
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
                            action: function (data) {
                                deleteCapacity(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "value"
            },
            {
                targets: 2,
                data: "unit"
            },
            {
                targets: 3,
                data: "power"
            },
            {
                targets: 4,
                data: "comment"
            },
            {
                targets: 5,
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
    function getCapacities() {
        
        dataTable.ajax.reload();
    }

    function deleteCapacity(capacity) {

        abp.message.confirm(
            "Are you sure you want to delete capacity?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _capacitieservice.deleteCapacity({
                        id: capacity.id
                    }).done(function () {
                        getCapacities();
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#AdvacedAuditFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#AdvacedAuditFiltersArea').slideUp();
    });

    $('#GetCapacitiesButton, #RefreshCapacitiesListButton').click(function (e) {
        e.preventDefault();
        getCapacities();
    });

})();