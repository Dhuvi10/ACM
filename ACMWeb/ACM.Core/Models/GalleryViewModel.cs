using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Models
{
   public class GalleryViewModel
    {

        public long Id { get; set; }
        public string StoreId { get; set; }
        public string Image { get; set; }
        public string ThumbnailImage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsMain { get; set; }
        public string FileName { get; set; }
        public long CheckInId { get; set; }
    }
}
