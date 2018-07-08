using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Models.CheckInContractViewModels
{
   public class ContractViewModel
    {
        public CheckInContractsViewModel CheckInModel { get; set; }
        public ProfileViewModel ProfileModel { get; set; }
    }
}
