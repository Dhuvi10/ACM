using ACM.Core.Models;
using ACM.Core.Models.CheckInContractViewModels;
using System.Collections.Generic;

namespace ACM.Core.Interfaces
{
    public interface IStoreManager
    {
        ResponseModel<StoreInfoViewModel> StoreLogo(string userId);
        ResponseModel<StoreInfoViewModel> SaveStoreLogo(StoreInfoViewModel model);

        ResponseModel<CheckInContractsViewModel> SaveCheckInContract(CheckInContractsViewModel model);
        ResponseModel<List<CheckInContractsViewModel>> ManageContractList();
        ResponseModel<List<StoreLogo>> StoreLogoList();
        ResponseModel<CheckInContractsViewModel> CheckInContractDetail(int contractId);

        ResponseModel<List<CheckInContractsViewModel>> ContractListByStore(string storeId);

        //      ResponseModel<List<CampaignVM>> CampaignList(string userId);
        //      ResponseModel<CampaignVM> EditCampaigns(int Id);
        //ResponseModel<List<CampaignVM>> CampaignListToMail();
        //ResponseModel<bool> UnsubscribeEmail(int ContactId);
        //ResponseModel<CampaignVM> GetCampaignByKey(string key);
        //ResponseModel<List<Contacts>> ContactList(string userId);
        //      APIResponse SaveCampaignContacts(CampaignVM campaignVM);


    }
}
