using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACM.Core.Models.RoleViewModels
{
    public class RoleViewModel
    {
        [Required]
        //[EmailAddress]
        [Display(Name = "Name")]
        public string Name { get; set; }
        //[Required]
       // [EmailAddress]
        [Display(Name = "NormalizedName")]
        public string NormalizedName { get; set; }

       // [Required]
       // [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [Display(Name = "ConcurrencyStamp")]
        public string ConcurrencyStamp { get; set; }

        public List<SelectListItem> roleList { get; set; }

        [Display(Name = "Roles")]
        public string Role { get; set; }

        [Display(Name = "Users")]
        public List<SelectListItem> userList { get; set; }
        public string User { get; set; }
    }
}
