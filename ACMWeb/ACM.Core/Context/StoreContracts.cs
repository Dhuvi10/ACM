using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class StoreContracts
    {
        public long Id { get; set; }
        public string StoreId { get; set; }
        public string CheckInContract { get; set; }
        public string CheckOutContract { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }

        public AspNetUsers Store { get; set; }
    }
}
