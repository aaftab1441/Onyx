﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface ICustomerServicePriceParameterAppService:IApplicationService
    {
        Task<GetServicePriceParameterForEditOutput> GeServicePriceParameterForEdit(GetServicePriceParametersForEditParamInput<int> input);
        Task<GetServicePriceParameterListOutput> GetServicePriceParameters(int customerServiceId);
        Task CreateOrUpdateServicePriceParameter(CreateOrUpdateServicePriceParameterInputDto input);
    }
}