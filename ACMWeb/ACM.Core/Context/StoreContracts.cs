using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class StoreContracts
    {
        public long Id { get; set; }
        public long? StoreId { get; set; }
        public string CheckInContract { get; set; }
        public string CheckInFormatingAbility { get; set; }
        public string CheckOutContract { get; set; }
        public string CheckOutFormatingAbility { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
