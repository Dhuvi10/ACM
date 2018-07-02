using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class StoreInfo
    {
        public long LogoId { get; set; }
        public string Logo { get; set; }
        public string StoreId { get; set; }

        public AspNetUsers Store { get; set; }
    }
}
