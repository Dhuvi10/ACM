using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACM.Core.Context
{
    public partial class Gallery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string StoreId { get; set; }
        public string Image { get; set; }
        public string ThumbnailImage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMain { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
