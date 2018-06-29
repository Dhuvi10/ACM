using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Models
{
    public class StoreInfoViewModel
    {
        public string StoreId { get; set; }
        public string Logo { get; set; }
        public string LogoName { get; set; }
        public Int64 LogoId { get; set; }

        public string UserId { get; set; }
    }
    public class StoreLogo
    {
        public string StoreId { get; set; }
        public string Logo { get; set; }
    }
}
