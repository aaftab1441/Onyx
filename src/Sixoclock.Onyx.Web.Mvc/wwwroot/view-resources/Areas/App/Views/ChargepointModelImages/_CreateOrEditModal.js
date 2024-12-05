(function () {
    app.modals.CreateOrEditChargepointModelImageModal = function () {

        var uploadedFileName = null;
        var $jcropApi = null;
        var _modalManager;
        var _chargepointModelImageService = abp.services.app.chargepointModelImage;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            $('#ChargepointModalImageForm input[name=ChargepointModelImage]').change(function () {
                
                $('#ChargepointModalImageForm').submit();
            });

            $('#ChargepointModalImageForm').ajaxForm({
                beforeSubmit: function (formData, jqForm, options) {

                    var $fileInput = $('#ChargepointModalImageForm input[name=ChargepointModelImage]');
                    var files = $fileInput.get()[0].files;

                    if (!files.length) {
                        return false;
                    }

                    var file = files[0];

                    //File type check
                    var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                    if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                        abp.message.warn(app.localize('ChargepointModelImage_Change_Error'));
                        return false;
                    }

                    //File size check
                    //if (file.size > 1048576) //1MB
                    //{
                    //    abp.message.warn(app.localize('ProfilePicture_Warn_SizeLimit'));
                    //    return false;
                    //}

                    return true;
                },
                success: function (response) {
                    if (response.success) {
                        
                        var $chargepointModelImageResize = $('#ChargepointModelImageResize');

                        var profileFilePath = abp.appPath + 'Images/Tenants/' + response.result.fileName + '?v=' + new Date().valueOf();
                        uploadedFileName = response.result.fileName;

                        if ($jcropApi) {
                            $jcropApi.destroy();
                        }

                        $chargepointModelImageResize.show();
                        $chargepointModelImageResize.attr('src', profileFilePath);
                        $chargepointModelImageResize.attr('originalWidth', response.result.width);
                        $chargepointModelImageResize.attr('originalHeight', response.result.height);

                        $chargepointModelImageResize.Jcrop({
                            setSelect: [0, 0, 100, 100],
                            aspectRatio: 1,
                            boxWidth: 400,
                            boxHeight: 400
                        }, function () {
                            $jcropApi = this;
                        });

                    } else {
                        abp.message.error(response.error.message);
                    }
                }
            });

            $('#ChargepointModelImageResize').hide();

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };

        this.save = function () {
            
            if (!_$form.valid()) {
                return;
            }

            if (!uploadedFileName) {
                return;
            }

            var resizeParams = {};
            if ($jcropApi) {
                resizeParams = $jcropApi.getSelection();
            }

            var containerWidth = $jcropApi.getContainerSize()[0];
            var containerHeight = $jcropApi.getContainerSize()[1];

            var originalWidth = containerWidth;
            var originalHeight = containerHeight;

            if ($('#ChargepointModelImageResize')) {
                originalWidth = parseInt($('#ChargepointModelImageResize').attr("originalWidth"));
                originalHeight = parseInt($('#ChargepointModelImageResize').attr("originalHeight"));
            }

            var widthRatio = originalWidth / containerWidth;
            var heightRatio = originalHeight / containerHeight;

            var chargepointModelImage = _$form.serializeFormToObject();

            chargepointModelImage.originalFileName = uploadedFileName;
            chargepointModelImage.x = parseInt(resizeParams.x * widthRatio);
            chargepointModelImage.y = parseInt(resizeParams.y * heightRatio);
            chargepointModelImage.width = parseInt(resizeParams.w * widthRatio);
            chargepointModelImage.height = parseInt(resizeParams.h * heightRatio);

            _modalManager.setBusy(true);

            _chargepointModelImageService.createOrUpdateChargepointModelImage(chargepointModelImage).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };

    //$('#ChargepointModelImage').on('change', function () {
        
    //    if (this.files && this.files[0]) {
    //        if (this.files[0].type.match(/^image\//)) {
    //            var reader = new FileReader();

    //            reader.onload = function (evt) {
    //                // var img = new Image();
    //                var canvas = document.getElementById('canvas');
                    
    //                while (canvas.hasChildNodes()) {
    //                    canvas.removeChild(canvas.lastChild);
    //                }

    //                var img = document.createElement("img");
    //                img.src = evt.target.result;
    //                img.width = 400;
    //                img.height = 400;
    //                canvas.appendChild(img);

    //            };

    //            reader.readAsDataURL(this.files[0]);
    //        }
    //        else {
    //            alert("Invalid file type! Please select an image file.");
    //        }
    //    }
    //    else {
    //        alert('No file(s) selected.');
    //    }
    //});

})();