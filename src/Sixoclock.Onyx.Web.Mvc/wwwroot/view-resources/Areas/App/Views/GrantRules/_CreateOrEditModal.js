(function () {
    app.modals.CreateOrEditRuleModal = function () {

        var _modalManager;
        var _ruleSetService = abp.services.app.ruleService;
        var _$ruleForm = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;


            _$ruleForm = _modalManager.getModal().find('form[name=RuleForm]');
            _$ruleForm.validate({ ignore: "" });
        };
        
        var entitySelect = $("#EntityName");
        var propertySelect = $("#PropertyName");
        var selectedValue = $("#Value").val();
        entitySelect.on('change', function () {
            _modalManager.setBusy(true);
            _ruleSetService.getProperties(entitySelect.find(":selected").text())
                .done(function (data) {
                    propertySelect.find('option')
                        .remove()
                        .end();

                    $.each(data, function (i, l) {
                        propertySelect.append($('<option>', {
                            value: l.displayText,
                            text: l.displayText,
                            type:l.type
                        }));
                    });
                    propertySelect.trigger('change');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                })
        });
        propertySelect.on('change', function() {
                
                var type = propertySelect.find(":selected").attr('type');
                if (type === "Int32" || type === "Float" || type === "Double" || type === "String" || type==="Single") {
                    $("#Value").replaceWith(
                        '<input class="form-control" type="text" name="Value" id="Value" value="' + selectedValue + '" required>');
                    selectedValue = '';
                }
                else if (type === "DateTime") {
                    $("#Value").replaceWith(
                        '<input id="Value" name="Value" class="form-control form-control-inline input-medium date-picker"  type="text" value="' + selectedValue +'">');
                    $("#Value").daterangepicker({
                        timePicker: false,
                        singleDatePicker: true
                    });
                    selectedValue = '';
                } else if (type === "TimeSpan") {
                    $("#Value").replaceWith(
                        '<input id="Value" name="Value" class="form-control timepicker timepicker-default"  type="text" value="' + selectedValue + '">');
                    $("#Value").daterangepicker({
                        timePicker: true,
                        singleDatePicker: true
                    });
                    selectedValue = '';
                } else {
                    _ruleSetService.getPropertyValues(type)
                        .done(function(data) {
                            $("#Value").replaceWith(
                                '<select id="Value" name="Value" class="form-control" ' +
                                '</select>');

                            $.each(data,
                                function (i, l) {
                                    $("#Value").append($('<option>',
                                        {
                                            value: l.displayText,
                                            text: l.displayText,
                                            type: l.type,
                                            selected:l.displayText===selectedValue
                                        }));
                                });
                            selectedValue = '';
                        })
                        .always(function() {
                           // _modalManager.setBusy(false);
                        });

                }
           
        });
        propertySelect.trigger('change');
       

        this.save = function () {
            if (!_$ruleForm.valid()) {
                return;
            }

            var rule = _$ruleForm.serializeFormToObject();
            _modalManager.setBusy(true);
            _ruleSetService.createOrUpdateRule({
                Rule: rule
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditRuleModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);

            });
        };
    };
})();