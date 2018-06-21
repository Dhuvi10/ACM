using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using System;
using System.Linq;


namespace ACM.Core.Managers
{
    public class ManageStore : IManageStore
    {
        private ACMContext acmContext;

        public ManageStore(ACMContext _acmContext)
        {
            acmContext = _acmContext;


        }

        public ResponseModel<StoreInfoViewModel> StoreLogo(string userId)
        {
            ResponseModel<StoreInfoViewModel> response = new ResponseModel<StoreInfoViewModel>();
            try
            {
                var model = acmContext.StoreInfo.Where(e => e.StoerId == userId).FirstOrDefault();
                if (model != null)
                {
                    response.Data.Logo = model.Logo;
                    response.Data.LogoId = model.LogoId;
                    response.Data.StoerId = model.StoerId;
                    // response.Data = list;
                    response.Status = true;
                }
                else
                {
                    response.Data = null;
                   response.Status = false;
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;

            }

            return response;
        }

        //public ResponseModel<List<CampaignVM>> CampaignList(string userId)
        //{
        //    ResponseModel<List<CampaignVM>> response = new ResponseModel<List<CampaignVM>>();
        //    try
        //    {
        //        var _list = leanContext.Campaigns.Where(e => e.CreatedBy == userId).Select(e => new CampaignVM
        //        {
        //            CampaignName = e.CampaignName,
        //            StartDate = e.StartDate,
        //            Emails = e.Emails.Select(c => c).ToList(),
        //            Contacts = e.Contacts.Select(co => co).ToList(),
        //            Id = e.Id,
        //            CreatedBy = e.CreatedBy,
        //            CreatedOn = e.CreatedOn,
        //            UpdatedBy = e.UpdatedBy,
        //            UpdatedOn = e.UpdatedOn,
        //            CampaignKey = e.CampaignKey


        //        }).ToList();
        //        response.Data = _list;
        //        response.Status = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.ErrorMessage = ex.Message;

        //    }

        //    return response;
        //}
        //public ResponseModel<int> SaveCampaigns(CampaignVM campaignVM)
        //{
        //    ResponseModel<int> result = new ResponseModel<int>();
        //    var campaignModel = leanContext.Campaigns.Where(e => e.Id == campaignVM.Id).Include(x => x.Emails).Include(x => x.Contacts).FirstOrDefault();
        //    try
        //    {
        //        if (campaignModel == null)
        //        {
        //            Campaigns model = new Campaigns();
        //            model.CampaignKey = campaignVM.CampaignKey;
        //            model.CampaignName = campaignVM.CampaignName;
        //            model.Contacts = campaignVM.Contacts;
        //            if (campaignVM.MailServices != null && campaignVM.MailServices.IsDefaultService == false)
        //                model.MailServices = campaignVM.MailServices;
        //            else
        //                model.MailServices = leanContext.MailServices.Where(x => x.IsDefaultService == true).FirstOrDefault();
        //            model.StartDate = campaignVM.StartDate;
        //            model.Emails = campaignVM.Emails;
        //            model.CreatedBy = campaignVM.CreatedBy;
        //            model.UpdatedBy = campaignVM.CreatedBy;
        //            model.CreatedOn = DateTime.Now;
        //            model.UpdatedOn = DateTime.Now;
        //            leanContext.Campaigns.Add(model);
        //            leanContext.SaveChanges();
        //            result.Status = true;
        //            result.Data = Convert.ToInt32(model.Id);
        //            result.Message = "Campaign Created";
        //        }
        //        else
        //        {
        //            List<Contacts> contactsUpdate = new List<Contacts>();
        //            List<Emails> emailsUpdate = new List<Emails>();
        //            campaignModel.CampaignName = campaignVM.CampaignName;
        //            foreach (var itemContacts in campaignVM.Contacts)
        //            {
        //                var contactModel = campaignModel.Contacts.Where(e => e.Id == itemContacts.Id).FirstOrDefault();
        //                if (contactModel == null)
        //                {
        //                    contactModel = new Contacts();
        //                }
        //                contactModel.FirstName = itemContacts.FirstName;
        //                contactModel.LastName = itemContacts.LastName;
        //                contactModel.EmailAddress = itemContacts.EmailAddress;
        //                contactModel.IsSubscribed = itemContacts.IsSubscribed;
        //                contactModel.Salutation = itemContacts.Salutation;
        //                contactsUpdate.Add(contactModel);
        //            }
        //            campaignModel.Contacts = contactsUpdate;
        //            // campaignModel.MailServices = campaignVM.MailServices;
        //            if (campaignVM.MailServices != null && campaignVM.MailServices.IsDefaultService == false)
        //                campaignModel.MailServices = campaignVM.MailServices;
        //            else
        //                campaignModel.MailServices = leanContext.MailServices.Where(x => x.IsDefaultService == true).FirstOrDefault();

        //            campaignModel.StartDate = campaignVM.StartDate;//DateTime.Now;
        //            foreach (var itemEmails in campaignVM.Emails)
        //            {
        //                var emailModel = campaignModel.Emails.Where(e => e.Id == itemEmails.Id).FirstOrDefault();
        //                if (emailModel != null)
        //                {
        //                    emailModel.MailSubject = itemEmails.MailSubject;
        //                    emailModel.MailBody = itemEmails.MailBody;
        //                    emailModel.MailOrder = itemEmails.MailOrder;
        //                    emailModel.Days = itemEmails.Days;
        //                    emailModel.Campaigns = emailModel.Campaigns;
        //                    emailsUpdate.Add(emailModel);
        //                }
        //                else
        //                {
        //                    Emails email = new Emails();
        //                    email.MailSubject = itemEmails.MailSubject;
        //                    email.MailBody = itemEmails.MailBody;
        //                    email.MailOrder = itemEmails.MailOrder;
        //                    email.Days = itemEmails.Days;
        //                    email.Campaigns = itemEmails.Campaigns;
        //                    emailsUpdate.Add(email);
        //                }
        //            }
        //            campaignModel.Emails = emailsUpdate;
        //            campaignModel.UpdatedBy = campaignVM.UpdatedBy;
        //            campaignModel.UpdatedOn = DateTime.Now;
        //            leanContext.SaveChanges();
        //            result.Status = true;
        //            result.Data = Convert.ToInt32(campaignModel.Id);
        //            result.Message = "Campaign Updated";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = false;
        //        result.ErrorMessage = ex.Message;

        //    }
        //    return result;

        //}
        //public ResponseModel<CampaignVM> EditCampaigns(int Id)
        //{
        //    ResponseModel<CampaignVM> result = new ResponseModel<CampaignVM>();
        //    try
        //    {
        //        var model = leanContext.Campaigns.Where(e => e.Id == Id).Include(x => x.Emails).Include(x => x.Contacts).Select(e => new CampaignVM
        //        {
        //            CampaignName = e.CampaignName,
        //            StartDate = e.StartDate,
        //            Emails = e.Emails.Select(c => c).ToList(),
        //            Contacts = e.Contacts.Select(co => co).ToList(),
        //            Id = e.Id,
        //            CreatedBy = e.CreatedBy,
        //            CreatedOn = e.CreatedOn,
        //            UpdatedBy = e.UpdatedBy,
        //            UpdatedOn = e.UpdatedOn,
        //            CampaignKey = e.CampaignKey,
        //            MailServices = e.MailServices.IsDefaultService ? new MailServices() { Id = e.MailServices.Id, IsDefaultService = e.MailServices.IsDefaultService } : e.MailServices,

        //        }).FirstOrDefault();
        //        result.Data = model;
        //        result.Status = true;
        //        result.Message = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}

        //public ResponseModel<CampaignVM> GetCampaignByKey(string key)
        //{
        //    ResponseModel<CampaignVM> result = new ResponseModel<CampaignVM>();
        //    try
        //    {
        //        var model = leanContext.Campaigns.Where(e => e.CampaignKey == key).Select(e => new CampaignVM
        //        {
        //            CampaignName = e.CampaignName,
        //            StartDate = e.StartDate,
        //            Emails = e.Emails.Select(c => c).ToList(),
        //            Contacts = e.Contacts.Select(co => co).ToList(),
        //            Id = e.Id,
        //            CreatedBy = e.CreatedBy,
        //            CreatedOn = e.CreatedOn,
        //            UpdatedBy = e.UpdatedBy,
        //            UpdatedOn = e.UpdatedOn,
        //            CampaignKey = e.CampaignKey,
        //            MailServices = e.MailServices.IsDefaultService ? new MailServices() { Id = e.MailServices.Id, IsDefaultService = e.MailServices.IsDefaultService } : e.MailServices,

        //        }).FirstOrDefault();
        //        if (model != null)
        //        {
        //            result.Data = model;
        //            result.Status = true;
        //            result.Message = "Success";
        //        }
        //        else
        //        {
        //            result.Data = null;
        //            result.Status = false;
        //            result.ErrorMessage = "Campaign not found.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;

        //}
        //public ResponseModel<List<CampaignVM>> CampaignListToMail()
        //{
        //    ResponseModel<List<CampaignVM>> response = new ResponseModel<List<CampaignVM>>();
        //    try
        //    {
        //        var _list = leanContext.Campaigns.Select(e => new CampaignVM
        //        {
        //            CampaignName = e.CampaignName,
        //            StartDate = e.StartDate,
        //            Emails = e.Emails.Select(c => c).ToList(),
        //            Contacts = e.Contacts.Select(co => co).ToList(),
        //            Id = e.Id,
        //            CreatedBy = e.CreatedBy,
        //            CreatedOn = e.CreatedOn,
        //            UpdatedBy = e.UpdatedBy,
        //            UpdatedOn = e.UpdatedOn,
        //            CampaignKey = e.CampaignKey,
        //            MailServices = e.MailServices
        //        }).Where(x => x.StartDate <= DateTime.Now).ToList();
        //        response.Data = _list;
        //        response.Status = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.ErrorMessage = ex.Message;

        //    }

        //    return response;
        //}

        //public ResponseModel<bool> UnsubscribeEmail(int ContactId)
        //{
        //    ResponseModel<bool> result = new ResponseModel<bool>();
        //    var contact = leanContext.Contacts.Where(x => x.Id == ContactId).FirstOrDefault();
        //    if (contact == null)
        //    {
        //        result.Status = false;
        //        result.Data = false;
        //        result.Message = "Unsubscribed failed!";
        //        return result;
        //    }
        //    contact.IsSubscribed = false;
        //    leanContext.SaveChanges();
        //    result.Status = true;
        //    result.Data = true;
        //    result.Message = "Unsubscribed Successful!";
        //    return result;
        //}

        //public APIResponse SaveCampaignContacts(CampaignVM campaignVM)
        //{
        //    APIResponse result = new APIResponse();
        //    var campaignModel = leanContext.Campaigns.Where(e => e.Id == campaignVM.Id).Include(x => x.Emails).Include(x => x.Contacts).FirstOrDefault();
        //    try
        //    {
        //        if (campaignModel != null)
        //        {
        //            int count = 0;
        //            List<Contacts> contactsUpdate = new List<Contacts>();
        //            foreach (var itemContacts in campaignVM.Contacts)
        //            {
        //                var contactModel = campaignModel.Contacts.Where(e => e.EmailAddress == itemContacts.EmailAddress).FirstOrDefault();
        //                if (contactModel == null)
        //                {
        //                    contactModel = new Contacts();
        //                }
        //                contactModel.FirstName = itemContacts.FirstName;
        //                contactModel.LastName = itemContacts.LastName;
        //                contactModel.IsSubscribed = itemContacts.IsSubscribed;
        //                contactModel.EmailAddress = itemContacts.EmailAddress;
        //                contactModel.Salutation = itemContacts.Salutation;
        //                contactsUpdate.Add(contactModel);
        //            }
        //            count = contactsUpdate.Count;
        //            foreach (var item in campaignModel.Contacts)
        //            {
        //                var exists = contactsUpdate.Where(x => x.EmailAddress == item.EmailAddress).FirstOrDefault();
        //                if (exists == null)
        //                {
        //                    contactsUpdate.Add(item);
        //                }
        //            }
        //            campaignModel.Contacts = contactsUpdate;
        //            campaignModel.UpdatedBy = campaignVM.UpdatedBy;
        //            campaignModel.UpdatedOn = DateTime.Now;
        //            leanContext.SaveChanges();
        //            result.Status = "Successful";
        //            result.Message = "Created[" + count + "] contacts.";
        //        }
        //        else
        //        {
        //            result.Status = "Unsuccessful";
        //            result.Message = "Campaign not found.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = "Unsuccessful";
        //        result.Message = "Internal server error.";
        //    }
        //    return result;

        //}
    }
}
