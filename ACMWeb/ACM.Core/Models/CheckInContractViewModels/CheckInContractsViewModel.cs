using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.Core.Models.CheckInContractViewModels
{
    public class CheckInContractsViewModel
    {
        public long Id { get; set; }
        [Required]
        [Display(Name="")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Required]
        [Display(Name = "Make")]
        public string Make { get; set; }
<<<<<<< HEAD
        public string Models { get; set; }
=======
        [Required]
        [Display(Name = "Models")]
        public string Models { get; set; }
        [Required]
        [Display(Name = "Vin")]
>>>>>>> 17bac5ee45b11e92c9bcc16d61aef04de1f2323a
        public string Vin { get; set; }
        [Required]
        [Display(Name = "Odo Meter")]
        public string OdoMeter { get; set; }
        [Required]
        [Display(Name = "Summery Of Task Completed")]
        public string SummeryOfTaskCompleted { get; set; }
        [Required]
        [Display(Name = "Parts Needed")]
        public string PartsNeeded { get; set; }
        [Required]
        [Display(Name = "Customer PartSupplied")]
        public string CustomerPartSupplied { get; set; }
        [Required]
        [Display(Name = " Personal Item In Vehicle")]
        public string PersonalItemInVehicle { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
       
        public string StoreId { get; set; }

    }

}
