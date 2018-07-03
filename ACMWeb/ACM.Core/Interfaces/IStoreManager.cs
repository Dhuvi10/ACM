using ACM.Core.Models;
using ACM.Core.Models.CheckInContractViewModels;
using System.Collections.Generic;

namespace ACM.Core.Interfaces
{
    public interface IStoreManager
    {
        ResponseModel<StoreInfoViewModel> StoreLogo(string userId);
        ResponseModel<StoreInfoViewModel> SaveStoreLogo(StoreInfoViewModel model);

        ResponseModel<CheckInContractsViewModel> SaveCheckInForm(CheckInContractsViewModel model);
        ResponseModel<List<CheckInContractsViewModel>> ManageContractList();
        ResponseModel<List<StoreLogo>> StoreLogoList();
        ResponseModel<CheckInContractsViewModel> CheckInContractDetail(int contractId);

        ResponseModel<List<CheckInContractsViewModel>> ContractListByStore(string storeId);
        ResponseModel<ManageContractViewModel> SaveContract(ManageContractViewModel model);
        ResponseModel<ManageContractViewModel> EditContract(string id);


    }
}
