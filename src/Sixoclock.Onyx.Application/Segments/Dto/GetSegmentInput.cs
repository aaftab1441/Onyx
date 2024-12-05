﻿using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Segments.Dto
{
    public class GetSegmentInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
