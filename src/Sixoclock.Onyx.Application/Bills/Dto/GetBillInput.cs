using System;
using System.Collections.Generic;
using System.Text;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;
using Abp.Extensions;

namespace Sixoclock.Onyx.Bills.Dto
{
    public class GetBillInput:PagedAndSortedInputDto,IShouldNormalize
    {
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Date DESC";
            }
        }
    }
}
