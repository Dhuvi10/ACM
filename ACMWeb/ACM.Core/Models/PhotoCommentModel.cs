using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Models
{
   public class PhotoCommentModel
    {
        public long Id { get; set; }
        public long PhotoId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }

}
