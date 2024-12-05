(function () {

    var _$mountTypesTable = $('#MountTypesTable');
    var _$filterForm = $('#MountTypeFilterForm');
    var _mountTypeService = abp.services.app.mountType;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/MountTypes/CreateOrEditMountTypeModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/MountTypes/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditMountTypeModal'
    });

    $('#CreateNewMountTypeButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    
    var dataTable = _$mountTypesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _mountTypeService.getMountType,
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
                                deleteMountType(data.record);
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
    function getMountTypes() {
        
        dataTable.ajax.reload();
    }

    function deleteMountType(mountType) {

        abp.message.confirm(
            "Are you sure you want to delete mountType?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _mountTypeService.deleteMountType({
                        id: mountType.id
                    }).done(function () {
                        getMountTypes(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#MountTypeFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#MountTypeFiltersArea').slideUp();
    });

    $('#GetMountTypesButton, #RefreshMountTypesListButton').click(function (e) {
        e.preventDefault();
        getMountTypes();
    });
    
})();