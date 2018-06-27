using ACM.Core.Models;
using ACM.Core.Models.ManageStoreContractViewModels;
using System.Collections.Generic;

namespace ACM.Core.Interfaces
{
    public interface IManageStore
    {
        ResponseModel<StoreInfoViewModel> StoreLogo(string userId);
        ResponseModel<StoreInfoViewModel> SaveStoreLogo(StoreInfoViewModel model);

        ResponseModel<ManageStoreContractsViewModel> SaveManageContract(ManageStoreContractsViewModel model);
        ResponseModel<List<ManageStoreContractsViewModel>> ManageContractList();
        //      ResponseModel<List<CampaignVM>> CampaignList(string userId);
        //      ResponseModel<CampaignVM> EditCampaigns(int Id);
        //ResponseModel<List<CampaignVM>> CampaignListToMail();
        //ResponseModel<bool> UnsubscribeEmail(int ContactId);
        //ResponseModel<CampaignVM> GetCampaignByKey(string key);
        //ResponseModel<List<Contacts>> ContactList(string userId);
        //      APIResponse SaveCampaignContacts(CampaignVM campaignVM);


    }
}
