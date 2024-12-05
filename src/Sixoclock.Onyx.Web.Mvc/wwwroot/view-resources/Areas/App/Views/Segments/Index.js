(function () {

    var _$segmentsTable = $('#SegmentsTable');
    var _$filterForm = $('#SegmentFilterForm');
    var _segmentService = abp.services.app.segment;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Segments/CreateOrEditSegmentModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Segments/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditSegmentModal'
    });

    var _segmentServicesModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Segments/SegmentServiceModel',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Segments/_SegmentServices.js',
        modalClass: 'SegmentServicesModel'
    });
    var _dashboardModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Segments/SegmentDashboardModal'
    });
    $('#CreateNewSegmentButton').click(function (e) {
        e.preventDefault();
        _createOrEditModal.open();
    });
    var _utilisationModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Segments/SegmentUtilisationModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Segments/_Utilisation.js',
        modalClass: 'SegmentUtilisationModal'
    });

    var dataTable = _$segmentsTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _segmentService.getSegment,
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
                            text: app.localize('Dashboard'),
                            action: function (data) {
                                _dashboardModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Utilisation'),
                            action: function (data) {
                                _utilisationModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Services'),
                            action: function (data) {
                               
                                _segmentServicesModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('DownloadExcel'),
                            action: function (data) {
                                _segmentService
                                    .getSegmentsToExcel({ id: data.record.id })
                                    .done(function (result) {
                                        app.downloadTempFile(result);
                                    });
                            }
                        }, {
                            text: app.localize('Delete'),
                            action: function (data) {
                                deleteSegment(data.record);
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
    function getSegments(reload) {

        dataTable.ajax.reload();
    }

    function deleteSegment(segment) {

        abp.message.confirm(
            "Are you sure you want to delete segment?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _segmentService.deleteSegment({
                        id: segment.id
                    }).done(function () {
                        getSegments(true);
                        abp.notify.success(app.localize('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#SegmentFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#SegmentFiltersArea').slideUp();
    });

    $('#GetSegmentsButton, #RefreshSegmentsListButton').click(function (e) {
        e.preventDefault();
        getSegments();
    });
    
})();