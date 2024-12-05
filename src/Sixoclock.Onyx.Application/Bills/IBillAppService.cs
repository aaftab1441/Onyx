using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sixoclock.Onyx.Bills.Dto;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Bills
{
    public interface IBillAppService:IApplicationService
    {
        Task<GetBillsListoutput> GetBillsList();
        Task AddComment(AddCommentInputDto input);
        Task<FileDto> GetBillsToExcel();
    }
}
