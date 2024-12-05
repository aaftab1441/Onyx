using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Bills.Dto
{
    public class BillListDto
    {
        public int Id { get; set; }
        public string BillStatus { get; set; }
        public string BillType { get; set; }
        public int Transactions { get; set; }
        public string Number { get; set; }
        public int Totalkwh { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Comment { get; set; }
    }
}
