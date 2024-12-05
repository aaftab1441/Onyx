(function ($) {
    app.modals.RegionUtilisationModal = function () {

        var _tagTransaction = abp.services.app.tagTransaction;
        var _modalManager;
        var trxBar;

        $('#datetimepicker').datepicker({
            format: 'mm/dd/yyyy'
        });
        
        _tagTransaction.getTransactionsUtilisationTotalByRegion({ id: $("#Id").val() }).done(function (result) {

            var today = new Date();
            if (result.length == 0) {

                var monthData = [],
                    countData = [],
                    deliveredData = [];
                for (var i = 0; i < daysInMonth(today.getMonth(), today.getFullYear()); i++) {
                    monthData.push({ date: today.addDays(i).getDate() });
                    countData.push({ count: 0 });
                    deliveredData.push({ kwhDelivered: 0 });
                }
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
                        categories: data,
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
                        data: countData

                    }, {
                        name: 'Delivered Kwh',
                        type: 'column',
                        data: deliveredData
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
        this.init = function (modalManager) {
            
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();            
        };
        
        Date.prototype.addDays = function (days) {
            var dat = new Date(this.valueOf());
            dat.setDate(dat.getDate() + days);
            return dat;
        }
        function daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
    };
})(jQuery);