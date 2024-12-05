﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface IInstallServiceAppService : IApplicationService
    {
       
        Task<GetServiceListOutput> GetInstallServices(EntityDto<int> installId);
        Task CreateOrUpdateService(CreateOrUpdateInstallServiceInputDto input);
        Task<GetInstallServiceForEditOutput> GetInstallServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);

    }
}