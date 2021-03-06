﻿using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class CheckInForm
    {
        public CheckInForm()
        {
            Gallery = new HashSet<Gallery>();
            ProfileInfo = new HashSet<ProfileInfo>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Models { get; set; }
        public string Vin { get; set; }
        public string OdoMeter { get; set; }
        public string SummeryOfTaskCompleted { get; set; }
        public string PartsNeeded { get; set; }
        public string CustomerPartSupplied { get; set; }
        public string PersonalItemInVehicle { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
        public string StoreId { get; set; }
        public bool? IsCheckOut { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public ICollection<Gallery> Gallery { get; set; }
        public ICollection<ProfileInfo> ProfileInfo { get; set; }
    }
}
