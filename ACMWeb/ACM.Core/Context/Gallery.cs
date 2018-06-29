using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class Gallery
    {
        public long Id { get; set; }
        public long? StoreId { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
