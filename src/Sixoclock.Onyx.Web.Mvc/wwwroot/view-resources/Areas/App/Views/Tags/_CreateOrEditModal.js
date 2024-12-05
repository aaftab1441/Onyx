(function ($) {
    app.modals.CreateOrEditTagModal = function () {

        var _modalManager;
        var _tagService = abp.services.app.tag;
        var _userService = abp.services.app.user;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };
        $('#datetimepicker').datepicker({
            format: 'dd/mm/yyyy'
        });
        this.save = function () {
            
            if (!_$form.valid()) {
                return;
            }
            
            var tag = _$form.serializeFormToObject();
            
            
            if (tag.UserId == "0") {
                
                sweetAlert("User is required please search user.");
                return;
            }
            _modalManager.setBusy(true);
            _tagService.createOrUpdateTag(tag).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };

        $('#txtUser').change(function () {

            _userService.getUserByName($('#txtUser').val()).done(function (result) {
                
                var htm = '<p> Name = ' + result.name + ' Email = ' + result.emailAddress + ' \n</p>';
                
                $("#UserInfo").html(htm);
                $("#UserId").val(result.id);
            })
        });

    };
})(jQuery);