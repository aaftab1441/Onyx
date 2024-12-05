(function () {

    var _$vendorErrorCodesTable = $('#VendorErrorCodesTable');
    var _$filterForm = $('#VendorErrorCodeFilterForm');
    var _vendorErrorCodeService = abp.services.app.vendorErrorCode;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/VendorErrorCodes/CreateOrEditVendorErrorCodeModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/VendorErrorCodes/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditVendorErrorCodeModal'
    });
    
    var dataTable = _$vendorErrorCodesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _vendorErrorCodeService.getVendorErrorCode,
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
                                deleteVendorErrorCode(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "vendorName"
            },
            {
                targets: 2,
                data: "errorCode"
            },
            {
                targets: 3,
                data: "errorText"
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
    function getVendorErrorCodes(reload) {

        dataTable.ajax.reload();
    }
    function createRequestParams() {
        var prms = {};
        _$filterForm.serializeArray().map(function (x) { prms[abp.utils.toCamelCase(x.name)] = x.value; });
        return prms;
    }
    function deleteVendorErrorCode(vendorErrorCode) {

        abp.message.confirm(
            "Are you sure you want to delete error code?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _vendorErrorCodeService.deleteVendorErrorCode({
                        id: vendorErrorCode.id
                    }).done(function () {
                        getVendorErrorCodes(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#VendorErrorCodeFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#VendorErrorCodeFiltersArea').slideUp();
    });

    $('#CreateNewVendorErrorCodeButton').click(function () {
        _createOrEditModal.open();
    });

    $('#GetVendorErrorCodesButton, #RefreshVendorErrorCodeListButton').click(function (e) {
        e.preventDefault();
        getVendorErrorCodes();
    });
    
})();