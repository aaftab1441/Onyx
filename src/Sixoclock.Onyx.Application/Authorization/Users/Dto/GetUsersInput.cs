﻿using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class GetUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public string Permission { get; set; }

        public int? Role { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name,Surname";
            }
        }
    }
}