(function ($) {
    
    var _tagTransaction = abp.services.app.tagTransaction;

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

    _tagTransaction.getTransactionsOverview().done(function (result) {
        $("#energyThisWeek").text(result.currentWeekEnergy == 0 || result.currentWeekEnergy == null ? "..." : result.currentWeekEnergy);
        $("#chargeTimeThisWeek").text(result.currentWeekChargetime == 0 || result.currentWeekChargetime == null ? "..." : result.currentWeekChargetime);
        $("#transactionsThisWeek").text(result.currentWeekTransactions == 0 || result.currentWeekTransactions == null ? "..." : result.currentWeekTransactions);
        $("#cO2SavedThisWeek").text(result.currentWeekCo2Saved == 0 || result.currentWeekCo2Saved == null ? "..." : result.currentWeekCo2Saved);

        $("#totalDeliveredEnergy").text(result.totalDeliveredEnergy == 0 || result.totalDeliveredEnergy == null ? "..." : result.totalDeliveredEnergy);
        $("#totalChargeTime").text(result.totalChargeTime == 0 || result.totalChargeTime == null ? "..." : result.totalChargeTime);
        $("#totalTransactions").text(result.totalTransactions == 0 || result.totalTransactions == null ? "..." : result.totalTransactions);
        $("#totalCO2Saved").text(result.totalCo2Saved == 0 || result.totalCo2Saved == null ? "..." : result.totalCo2Saved);

        $("#energyThisWeekChange").text(result.currentWeekEnergyChange == 0 || result.currentWeekEnergyChange == null ? "0" : result.currentWeekEnergyChange);
        $("#chargeTimeThisWeekChange").text(result.currentWeekChargetimeChange == 0 || result.currentWeekChargetimeChange == null ? "0" : result.currentWeekChargetimeChange);
        $("#transactionsThisWeekChange").text(result.currentWeekTransactionsChange == 0 || result.currentWeekTransactionsChange == null ? "0" : result.currentWeekTransactionsChange);
        $("#cO2SavedThisWeekChange").text(result.currentWeekCo2SavedChange == 0 || result.currentWeekCo2SavedChange == null ? "0" : result.currentWeekCo2SavedChange);
    });

    Date.prototype.addDays = function (days) {
        var dat = new Date(this.valueOf());
        dat.setDate(dat.getDate() + days);
        return dat;
    }
})(jQuery);