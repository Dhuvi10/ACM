using ACM.Core.Models;
using ACM.Core.Models.CheckInContractViewModels;
using ACM.Core.Models.UserViewModel;
using System.Collections.Generic;

namespace ACM.Core.Interfaces
{
    public interface IStoreManager
    {
        ResponseModel<UserViewModel> StoreLogo(string userId);
        ResponseModel<StoreInfoViewModel> SaveStoreLogo(StoreInfoViewModel model);

        ResponseModel<ContractViewModel> SaveCheckInForm(ContractViewModel model, string imagePath, string signPath);
        ResponseModel<List<CheckInContractsViewModel>> HistoryList(bool isCheckOut, string StoreId);
        ResponseModel<List<StoreLogo>> StoreLogoList();
        ResponseModel<CheckInContractsViewModel> CheckInContractDetail(int contractId);
        ResponseModel<StoreInfoViewModel> WebStoreLogo(string userId);
        ResponseModel<List<CheckInContractsViewModel>> ContractListByStore(string storeId);
        ResponseModel<ManageContractViewModel> SaveContract(ManageContractViewModel model);
        ResponseModel<ManageContractViewModel> EditContract(string id);
        ResponseModel<CheckInContractsViewModel> CheckOutForm(int contractId);


    }
}
