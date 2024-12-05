using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Bills.Dto
{
    public class GetBillsListoutput
    {
        public IEnumerable<BillListDto> Bills { get; set; }
    }
}
