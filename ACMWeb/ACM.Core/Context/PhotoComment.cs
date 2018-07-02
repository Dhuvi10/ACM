using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class PhotoComment
    {
        public long Id { get; set; }
        public long? PhotoId { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }

        public Gallery Photo { get; set; }
    }
}
