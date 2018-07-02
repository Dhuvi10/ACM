using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class ProfileInfo
    {
        public long Id { get; set; }
        public string StoreId { get; set; }
        public string Signature { get; set; }
        public string Photo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }

        public AspNetUsers Store { get; set; }
    }
}
