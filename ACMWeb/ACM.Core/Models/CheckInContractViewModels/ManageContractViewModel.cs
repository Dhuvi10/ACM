using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.Core.Models.CheckInContractViewModels
{
    public class ManageContractViewModel
    {
       [Display(Name = "Check In Contract")]
        public string CheckInContract { get; set; }
        [Display(Name = "CheckOut Contract")]
        public string CheckOutContract { get; set; }
    }
}
