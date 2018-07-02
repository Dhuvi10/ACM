using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class Gallery
    {
        public Gallery()
        {
            PhotoComment = new HashSet<PhotoComment>();
        }

        public long Id { get; set; }
        public string StoreId { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsMain { get; set; }
        public string ThumbnailImage { get; set; }

        public AspNetUsers Store { get; set; }
        public ICollection<PhotoComment> PhotoComment { get; set; }
    }
}
