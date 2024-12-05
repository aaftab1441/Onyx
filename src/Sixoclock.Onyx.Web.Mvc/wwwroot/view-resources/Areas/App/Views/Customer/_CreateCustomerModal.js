(function ($) {
    $.validator.addMethod("tenancyNameRegex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, app.localize('TenantName_Regex_Description'));
    app.modals.CreateCustomerModal = function () {

        var _modalManager;
        var _customerService = abp.services.app.customer;
        var _$form = null;
        var _passwordComplexityHelper = new app.PasswordComplexityHelper();
        this.init = function (modalManager) {
            _modalManager = modalManager;
            var modal = _modalManager.getModal();
            _$form = modal.find('form');
            _$form.validate(
                //{
                //rules: {
                //    TenancyName: {
                //        tenancyNameRegex: new RegExp(_$form.find('input[name=TenancyName]').attr('regex'))
                //    }
                //}
                //}
            );
            var passwordInputs = modal.find('input[name=AdminPassword],input[name=AdminPasswordRepeat]');
            var passwordInputGroups = passwordInputs.closest('.form-group');
            debugger;
          //  _passwordComplexityHelper.setPasswordComplexityRules(passwordInputs, window.passwordComplexitySetting);

            $('#CreateTenant_SetRandomPassword').change(function () {
                if ($(this).is(':checked')) {
                    passwordInputGroups.slideUp('fast');
                    passwordInputs.removeAttr('required');
                } else {
                    passwordInputGroups.slideDown('fast');
                    passwordInputs.attr('required', 'required');
                }
            });
        };
       
        this.save = function () {
            
            if (!_$form.valid()) {
                return;
            }
            
            var customer = _$form.serializeFormToObject();

            _modalManager.setBusy(true);

            _customerService.createCustomer(customer).done(function () {
                _modalManager.close();
                location.reload();
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);