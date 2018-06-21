using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class StoreInfo
    {
        public string StoerId { get; set; }
        public string Logo { get; set; }
        public long LogoId { get; set; }

        public AspNetUsers Stoer { get; set; }
    }
}
