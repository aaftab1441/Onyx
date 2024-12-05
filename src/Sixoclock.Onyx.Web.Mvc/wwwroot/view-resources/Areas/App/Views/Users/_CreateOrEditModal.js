(function ($) {
    app.modals.CreateOrEditUserModal = function () {

        var _userService = abp.services.app.user;
        var _tagTransaction = abp.services.app.tagTransaction;
        var _modalManager;
        var _$userInformationForm = null;
        var _passwordComplexityHelper = new app.PasswordComplexityHelper();
        var _organizationTree;
        var _$transactionsTable = $('#TransactionsTable');
        var _$filterForm = $('#TransactionsFilterForm');

        function _findAssignedRoleNames() {
            var assignedRoleNames = [];

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .each(function () {
                    if ($(this).is(':checked')) {
                        assignedRoleNames.push($(this).attr('name'));
                    }
                });

            return assignedRoleNames;
        }
        $('#datetimepicker').datepicker({
            format: 'mm/dd/yyyy'
        });
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _organizationTree = new OrganizationTree();
            _organizationTree.init(_modalManager.getModal().find('.organization-tree'));

            _$userInformationForm = _modalManager.getModal().find('form[name=UserInformationsForm]');
            _$userInformationForm.validate();

            _$tagsForm = _modalManager.getModal().find('form[name=TagsForm]');
            _$billingForm = _modalManager.getModal().find('form[name=BillingForm]');

            var passwordInputs = _modalManager.getModal().find('input[name=Password],input[name=PasswordRepeat]');
            var passwordInputGroups = passwordInputs.closest('.form-group');

            _passwordComplexityHelper.setPasswordComplexityRules(passwordInputs, window.passwordComplexitySetting);

            $('#EditUser_SetRandomPassword').change(function () {
                if ($(this).is(':checked')) {
                    passwordInputGroups.slideUp('fast');
                    if (!_modalManager.getArgs().id) {
                        passwordInputs.removeAttr('required');
                    }
                } else {
                    passwordInputGroups.slideDown('fast');
                    if (!_modalManager.getArgs().id) {
                        passwordInputs.attr('required', 'required');
                    }
                }
            });

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .change(function () {
                    $('#assigned-role-count').text(_findAssignedRoleNames().length);
                    
                });

            _modalManager.getModal().find('[data-toggle=tooltip]').tooltip();

        };
        _tagTransaction.getTransactionsTotal({ id: $("#Id").val() }).done(function (result) {
            
            var today = new Date();
            if (result.length == 0) {

                $('#container').highcharts({
                    chart: {
                        zoomType: 'xy'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Transaction Totals'
                    },
                    xAxis: [{
                        categories: [today.getDate(), today.addDays(1).getDate(), today.addDays(2).getDate(), today.addDays(3).getDate(), today.addDays(4).getDate(), today.addDays(5).getDate(), today.addDays(6).getDate()],
                        crosshair: true
                    }],
                    yAxis: [{ // Primary yAxis
                        labels: {
                            format: '{value}',
                            style: {
                                color: Highcharts.getOptions().colors[1]
                            }
                        },
                        title: {
                            text: 'Delivered Kwh',
                            style: {
                                color: Highcharts.getOptions().colors[1]
                            }
                        }
                    }, { // Secondary yAxis
                        title: {
                            text: 'Count',
                            style: {
                                color: Highcharts.getOptions().colors[0]
                            }
                        },
                        labels: {
                            format: '{value}',
                            style: {
                                color: Highcharts.getOptions().colors[0]
                            }
                        },
                        opposite: true
                    }],
                    tooltip: {
                        shared: true
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'left',
                        x: 120,
                        verticalAlign: 'top',
                        y: 100,
                        floating: true,
                        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                    },
                    series: [{
                        name: 'Count',
                        type: 'column',
                        yAxis: 1,
                        data: [0, 0, 0, 0, 0, 0, 0]

                    }, {
                        name: 'Delivered Kwh',
                        type: 'column',
                        data: [0, 0, 0, 0, 0, 0, 0]
                    }]
                });
            }
            else {
                $('#container').highcharts({
                    chart: {
                        zoomType: 'xy'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Transaction Totals'
                    },
                    xAxis: [{
                        categories: result.map(a => a.date),
                        crosshair: true
                    }],
                    yAxis: [{ // Primary yAxis
                        labels: {
                            format: '{value}',
                            style: {
                                color: Highcharts.getOptions().colors[1]
                            }
                        },
                        title: {
                            text: 'Delivered Kwh',
                            style: {
                                color: Highcharts.getOptions().colors[1]
                            }
                        }
                    }, { // Secondary yAxis
                        title: {
                            text: 'Count',
                            style: {
                                color: Highcharts.getOptions().colors[0]
                            }
                        },
                        labels: {
                            format: '{value}',
                            style: {
                                color: Highcharts.getOptions().colors[0]
                            }
                        },
                        opposite: true
                    }],
                    tooltip: {
                        shared: true
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'left',
                        x: 120,
                        verticalAlign: 'top',
                        y: 100,
                        floating: true,
                        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                    },
                    series: [{
                        name: 'Count',
                        type: 'column',
                        yAxis: 1,
                        data: result.map(a => parseInt(a.transactionsCount))

                    }, {
                        name: 'Delivered Kwh',
                        type: 'column',
                        data: result.map(a => parseInt(a.kwhDelivered))
                    }]
                });
            }
            
        });
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            
            $(window).trigger('resize');
        });
        Date.prototype.addDays = function (days) {
            var dat = new Date(this.valueOf());
            dat.setDate(dat.getDate() + days);
            return dat;
        }
        this.save = function () {

            if (!_$userInformationForm.valid()) {
                return;
            }

            var assignedRoleNames = _findAssignedRoleNames();
            var user = _$userInformationForm.serializeFormToObject();
            var tag = _$tagsForm.serializeFormToObject();
            var billing = _$billingForm.serializeFormToObject();

            if (user.SetRandomPassword) {
                user.Password = null;
            }

            _modalManager.setBusy(true);
            _userService.createOrUpdateUser({
                user: user,
                tag: tag,
                billing: billing,
                assignedRoleNames: assignedRoleNames,
                sendActivationEmail: user.SendActivationEmail,
                SetRandomPassword: user.SetRandomPassword,
                organizationUnits: _organizationTree.getSelectedOrganizations()
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditUserModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
        
        var dataTable = _$transactionsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _tagTransaction.getTransactions,
                inputFilter: function () {
                    return createRequestParams();
                }
            },
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: ''
                },
                {
                    targets: 0,
                    data: "date",
                    render: function (date) {
                        return moment(date).format('L');
                    }
                },
                {
                    targets: 1,
                    data: "startDate",
                    render: function (startDate) {
                        return moment(startDate).format('L');
                    }
                },
                {
                    targets: 2,
                    data: "stopDate",
                    render: function (stopDate) {
                        if (stopDate != null)
                            return moment(stopDate).format('L');
                        else
                            return "Ongoing";
                    }
                },
                {
                    targets: 3,
                    data: "duration",
                    render: function (duration) {
                        if (duration != "Ongoing")
                            return moment(duration).format('L');
                        else
                            return duration;
                    }
                },
                {
                    targets: 4,
                    data: "kwh",
                    render: function (kwh) {
                        if (kwh == null)
                            return "Ongoing"
                        else
                            return kwh;
                    }
                },
                {
                    targets: 5,
                    data: "installation"
                },
                {
                    targets: 6,
                    data: "group"
                },
                {
                    targets: 7,
                    data: "charger"
                },
                {
                    targets: 8,
                    data: "evse"
                },
                {
                    targets: 9,
                    data: "user"
                },
                {
                    targets: 10,
                    data: "billKwh"
                },
                {
                    targets: 11,
                    data: "billMin"
                },
                {
                    targets: 12,
                    data: "cost"
                },
                {
                    targets: 13,
                    data: "toBilled"
                },
                {
                    targets: 14,
                    data: "earned"
                }
            ]
        });
        function createRequestParams() {
            var prms = {};
            _$filterForm.serializeArray().map(function (x) { prms[abp.utils.toCamelCase(x.name)] = x.value; });
            return prms;
        }
    };
})(jQuery);