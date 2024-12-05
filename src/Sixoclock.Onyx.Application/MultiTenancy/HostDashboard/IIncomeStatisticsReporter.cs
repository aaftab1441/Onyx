using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sixoclock.Onyx.MultiTenancy.HostDashboard.Dto;

namespace Sixoclock.Onyx.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}