(function () {

    var _$chargepointModelImagesTable = $('#ChargepointModelImagesTable');
    var _chargepointModelImageService = abp.services.app.chargepointModelImage;
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/ChargepointModelImages/CreateOrEditChargepointModelImageModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ChargepointModelImages/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditChargepointModelImageModal'
    });


    var dataTable = _$chargepointModelImagesTable.DataTable({

        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _chargepointModelImageService.getChargepointModelImage
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
                                deleteChargepointModelImage(data.record);
                            }
                        }
                    ]
                }
            },
            {
                targets: 1,
                data: "comment"
            },
            {
                targets: 2,
                data: "isActive"
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
    
    function getChargepointModelImages(reload) {
        
        if (reload) {
            dataTable.ajax.reload();
        } else {
            dataTable.ajax.reload({
                filter: $('#ChargepointModelImagesTableFilter').val(),
                comment: $("#comment").val(),
                isActive: $("#isActive").val()
            });
        }
    }

    function deleteChargepointModelImage(chargepointModelImage) {

        abp.message.confirm(
            "Are you sure you want to delete chargepointmodel image?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _chargepointModelImageService.deleteChargepointModelImage({
                        id: chargepointModelImage.id
                    }).done(function () {
                        getChargepointModelImages(true);
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

    $('#CreateNewChargepointModelImageButton').click(function () {
        _createOrEditModal.open();
    });

    $('#GetChargepointModelImagesButton, #RefreshChargepointModelImageListButton').click(function (e) {
        e.preventDefault();
        getChargepointModelImages();
    });

})();