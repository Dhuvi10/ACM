using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Models
{
   public class ProfileViewModel
    {
        public long Id { get; set; }
        public long CheckInId { get; set; }
        public string Signature { get; set; }
        public string Photo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }

    }
}
