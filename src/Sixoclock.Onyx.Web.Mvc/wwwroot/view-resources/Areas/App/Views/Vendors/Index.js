(function () {

    var _$vendorsTable = $('#VendorsTable');
    var _$filterForm = $('#VendorFilterForm');
    var _vendorService = abp.services.app.vendor;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Vendors/CreateOrEditVendorModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Vendors/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditVendorModal'
    });

    $('#CreateNewVendorButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });

    var dataTable = _$vendorsTable.DataTable({
        
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _vendorService.getVendor,
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
                                deleteVendor(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "name"
            },
            {
                targets: 2,
                data: "comment"
            },
            {
                targets: 3,
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
    function getVendors() {

        dataTable.ajax.reload();
    }

    function deleteVendor(vendor) {

        abp.message.confirm(
            "Are you sure you want to delete vendor?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _vendorService.deleteVendor({
                        id: vendor.id
                    }).done(function () {
                        getVendors();
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#VendorFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#VendorFiltersArea').slideUp();
    });

    $('#GetVendorsButton, #RefreshVendorsListButton').click(function (e) {
        e.preventDefault();
        getVendors();
    });

})();