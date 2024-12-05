using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixoclock.Onyx.Bills.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Bills
{
    public class AddCommentModalViewModel
    {
        public AddCommentInputDto Bill { get; set; }
        public bool IsEditMode => true;

        public AddCommentModalViewModel(AddCommentInputDto input)
        {
            Bill = input;
        }
    }
}
