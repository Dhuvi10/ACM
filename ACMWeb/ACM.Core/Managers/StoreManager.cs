using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.CheckInContractViewModels;
using ACM.Core.Models.UserViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ACM.Core.Managers
{
    public class StoreManager : IStoreManager
    {
        private ACMContext acmContext;
        private IProfileManager profileManager;
        public StoreManager(ACMContext _acmContext, IProfileManager _profileManager)
        {
            acmContext = _acmContext;
            profileManager = _profileManager;
        }
        public ResponseModel<List<StoreLogo>> StoreLogoList()
        {
            ResponseModel<List<StoreLogo>> response = new ResponseModel<List<StoreLogo>> { Data = new List<StoreLogo>() };
            try
            {
                var model = acmContext.StoreInfo.Select(x => new StoreLogo { StoreId = x.StoreId, Logo = x.Logo }).ToList();
                if (model != null)
                {
                    response.Data = model;
                    // response.Data = list;
                    response.Status = true;
                }
                else
                {
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
        public ResponseModel<UserViewModel> StoreLogo(string userId)
        {
            ResponseModel<UserViewModel> response = new ResponseModel<UserViewModel> { Data = new UserViewModel() };
            try
            {

                var model = acmContext.AspNetUsers.Where(e => e.Id == userId).FirstOrDefault();
                if (model != null)
                {
                    response.Data.Email = model.Email;
                    response.Data.id = model.Id;
                    response.Data.Logo = model.StoreInfo.Select(e => e.Logo).FirstOrDefault();
                    response.Data.suspened = true;
                    // response.Data = list;
                    response.Status = true;
                }
                else
                {

                    //response.Data.StoreId = model.StoreId == null ? userId : model.StoreId;
                    // response.Data = list;
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
        public ResponseModel<StoreInfoViewModel> SaveStoreLogo(StoreInfoViewModel _model)
        {
            ResponseModel<StoreInfoViewModel> response = new ResponseModel<StoreInfoViewModel>();
            try
            {
                var model = acmContext.StoreInfo.Where(e => e.StoreId == _model.StoreId).FirstOrDefault();
                if (model != null)
                {
                    model.Logo = _model.LogoName;
                    // model.LogoId = _model.LogoId;
                    model.StoreId = _model.StoreId;

                    acmContext.SaveChanges();

                    // response.Data = list;
                    response.Status = true;
                    response.Message="success";
                }
                else
                {
                    StoreInfo storeModel = new StoreInfo();
                    storeModel.Logo = _model.LogoName;
                    //storeModel.LogoId = model.LogoId;
                    storeModel.StoreId = _model.StoreId;
                    acmContext.Add(storeModel);
                    acmContext.SaveChanges();
                    response.Data = null;
                    response.Message = "success";
                    response.Status = true;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public ResponseModel<ContractViewModel> SaveCheckInForm(ContractViewModel _model, string imagePath, string signPath)
        {

            ResponseModel<ContractViewModel> response = new ResponseModel<ContractViewModel> { Data = new ContractViewModel() };
            try
            {
                var model = acmContext.CheckInForm.Where(e => e.Id == _model.CheckInModel.Id).FirstOrDefault();
                if (model != null)
                {
                    model.Id = _model.CheckInModel.Id;
                    model.SummeryOfTaskCompleted = _model.CheckInModel.SummeryOfTaskCompleted;
                    model.EmailAddress = _model.CheckInModel.EmailAddress;
                    model.Name = _model.CheckInModel.Name;
                    model.Vin = _model.CheckInModel.Vin;
                    model.Year = _model.CheckInModel.Year;
                    model.PartsNeeded = _model.CheckInModel.PartsNeeded;
                    model.PersonalItemInVehicle = _model.CheckInModel.PersonalItemInVehicle;
                    model.PhoneNumber = _model.CheckInModel.PhoneNumber;
                    model.CustomerPartSupplied = _model.CheckInModel.CustomerPartSupplied;
                    model.OdoMeter = _model.CheckInModel.OdoMeter;
                    model.Models = _model.CheckInModel.Models;
                    model.Make = _model.CheckInModel.Make;
                    model.CreatedOn = DateTime.Now;
                    model.IsActive = true;
                    model.IsCheckOut = false;
                    model.StoreId = _model.CheckInModel.StoreId;
                    acmContext.SaveChanges();
                    if (!string.IsNullOrEmpty(_model.ProfileModel.Signature))
                    {
                        _model.ProfileModel.CheckInId = model.Id;
                        var signResponse = profileManager.UpdateProfileDetail(_model.ProfileModel, imagePath, signPath);
                        if (signResponse.Status)
                        {
                            response.Data.ProfileModel = signResponse.Data;
                        }
                    }
                    // response.Data = list;
                    response.Data.CheckInModel = _model.CheckInModel;
                    response.Data = _model;
                    response.Message = "Record updated successfully";
                    response.Status = true;
                }
                else
                {
                    CheckInForm modelCheckInForm = new CheckInForm();
                    modelCheckInForm.SummeryOfTaskCompleted = _model.CheckInModel.SummeryOfTaskCompleted;
                    modelCheckInForm.EmailAddress = _model.CheckInModel.EmailAddress;
                    modelCheckInForm.Name = _model.CheckInModel.Name;
                    modelCheckInForm.Vin = _model.CheckInModel.Vin;
                    modelCheckInForm.Year = _model.CheckInModel.Year;
                    modelCheckInForm.PartsNeeded = _model.CheckInModel.PartsNeeded;
                    modelCheckInForm.PersonalItemInVehicle = _model.CheckInModel.PersonalItemInVehicle;
                    modelCheckInForm.PhoneNumber = _model.CheckInModel.PhoneNumber;
                    modelCheckInForm.OdoMeter = _model.CheckInModel.OdoMeter;
                    modelCheckInForm.Models = _model.CheckInModel.Models;
                    modelCheckInForm.Make = _model.CheckInModel.Make;
                    modelCheckInForm.CustomerPartSupplied = _model.CheckInModel.CustomerPartSupplied;
                    modelCheckInForm.CreatedOn = DateTime.Now;
                    modelCheckInForm.IsActive = true;
                    modelCheckInForm.StoreId = _model.CheckInModel.StoreId;
                    modelCheckInForm.IsCheckOut = false;
                    acmContext.Add(modelCheckInForm);
                    acmContext.SaveChanges();

                    if (!string.IsNullOrEmpty(_model.ProfileModel.Signature))
                    {
                        _model.ProfileModel.CheckInId = modelCheckInForm.Id;
                        var signResponse = profileManager.AddProfileDetail(_model.ProfileModel, imagePath, signPath);
                        if (signResponse.Status)
                        {
                            response.Data.ProfileModel = signResponse.Data;
                        }
                    }

                    _model.CheckInModel.Id = modelCheckInForm.Id;
                    _model.CheckInModel.StoreId = modelCheckInForm.StoreId;
                    response.Data.CheckInModel = _model.CheckInModel;


                   // response.Data = _model;
                    response.Message = "Record saved successfully";
                    response.Status = true;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
            // return responseModel;

        }
        public ResponseModel<CheckInContractsViewModel> CheckOutForm(int contractId)
        {
            ResponseModel<CheckInContractsViewModel> response = new ResponseModel<CheckInContractsViewModel> { Data = new CheckInContractsViewModel() };
            try
            {
                var model = acmContext.CheckInForm.Where(e => e.Id == contractId).FirstOrDefault();
                if (model != null)
                {
                    model.Id = contractId;
                    model.IsCheckOut = true;
                    model.CheckOutDate = DateTime.Now;
                    acmContext.SaveChanges();

                    // response.Data = list;
                    response.Data = new CheckInContractsViewModel() { Id=model.Id,CheckOutDate=model.CheckOutDate};
                    response.Message = "CheckOut successfully";
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public ResponseModel<List<CheckInContractsViewModel>> HistoryList(bool isCheckOut,string StoreId)
        {
            ResponseModel<List<CheckInContractsViewModel>> response = new ResponseModel<List<CheckInContractsViewModel>> { Data = new List<CheckInContractsViewModel>() };
            try
            {
                var _list = acmContext.CheckInForm.Where(x=>x.IsCheckOut== isCheckOut && x.StoreId== StoreId).Select(e => e).ToList();
                foreach (var item in _list)
                {
                    CheckInContractsViewModel model = new CheckInContractsViewModel();
                    model.Id = item.Id;
                    model.SummeryOfTaskCompleted = item.SummeryOfTaskCompleted;
                    model.EmailAddress = item.EmailAddress;
                    model.Name = item.Name;
                    model.Vin = item.Vin;
                    model.Year = item.Year;
                    model.PartsNeeded = item.PartsNeeded;
                    model.PersonalItemInVehicle = item.PersonalItemInVehicle;
                    model.CustomerPartSupplied = item.CustomerPartSupplied;
                    model.PhoneNumber = item.PhoneNumber;
                    model.OdoMeter = item.OdoMeter;
                    model.Models = item.Models;
                    model.Make = item.Make;
                    model.CreatedOn = item.CreatedOn;
                    model.CreatedDate = item.CreatedOn.ToString();
                    model.IsActive = item.IsActive;
                    model.StoreId = item.StoreId;
                    if (isCheckOut)
                    {
                        model.IsCheckOut = item.IsCheckOut;
                        model.CheckOutDate = item.CheckOutDate;
                        model.DateCheckOut = item.CheckOutDate.ToString();
                    }
                    response.Data.Add(model);
                }
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseModel<List<CheckInContractsViewModel>> ContractListByStore(string storeId)
        {
            ResponseModel<List<CheckInContractsViewModel>> response = new ResponseModel<List<CheckInContractsViewModel>> { Data = new List<CheckInContractsViewModel>() };
            try
            {
                var _list = acmContext.CheckInForm.Where(k=>k.StoreId==storeId && k.IsCheckOut==false).Select(e => e).ToList();
                foreach (var item in _list)
                {
                    CheckInContractsViewModel model = new CheckInContractsViewModel();
                    model.Id = item.Id;
                    model.SummeryOfTaskCompleted = item.SummeryOfTaskCompleted;
                    model.EmailAddress = item.EmailAddress;
                    model.Name = item.Name;
                    model.Vin = item.Vin;
                    model.Year = item.Year;
                    model.PartsNeeded = item.PartsNeeded;
                    model.PersonalItemInVehicle = item.PersonalItemInVehicle;
                    model.CustomerPartSupplied = item.CustomerPartSupplied;
                    model.PhoneNumber = item.PhoneNumber;
                    model.OdoMeter = item.OdoMeter;
                    model.Models = item.Models;
                    model.Make = item.Make;
                    model.CreatedOn = item.CreatedOn;
                    model.CreatedDate = item.CreatedOn.ToString();
                    model.IsActive = item.IsActive;
                    model.StoreId = item.StoreId;
                    response.Data.Add(model);
                }
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public ResponseModel<CheckInContractsViewModel> CheckInContractDetail(int contractId)
        {
            ResponseModel<CheckInContractsViewModel> model = new ResponseModel<CheckInContractsViewModel> { Data = new CheckInContractsViewModel() };
            try
            {
                var _model = acmContext.CheckInForm.Where(e => e.Id == contractId).FirstOrDefault();
                if (_model != null)
                {
                    model.Data.Id = _model.Id;
                    model.Data.SummeryOfTaskCompleted = _model.SummeryOfTaskCompleted;
                    model.Data.EmailAddress = _model.EmailAddress;
                    model.Data.Name = _model.Name;
                    model.Data.Vin = _model.Vin;
                    model.Data.Year = _model.Year;
                    model.Data.PartsNeeded = _model.PartsNeeded;
                    model.Data.PersonalItemInVehicle = _model.PersonalItemInVehicle;
                    model.Data.CustomerPartSupplied = _model.CustomerPartSupplied;
                    model.Data.PhoneNumber = _model.PhoneNumber;
                    model.Data.OdoMeter = _model.OdoMeter;
                    model.Data.Models = _model.Models;
                    model.Data.Make = _model.Make;
                    model.Data.CreatedOn = _model.CreatedOn;
                    model.Data.IsActive = _model.IsActive;
                    model.Data.CreatedDate = _model.CreatedOn.ToString();
                    model.Data.StoreId = _model.StoreId;
                    model.Data.Signature = _model.ProfileInfo.Where(x => x.CheckInId == contractId).FirstOrDefault().Signature;
                }
                model.Status = true;
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public ResponseModel<ManageContractViewModel> SaveContract(ManageContractViewModel model)
        {
            ResponseModel<ManageContractViewModel> response = new ResponseModel<ManageContractViewModel> { Data = new ManageContractViewModel() };
            try
            {
                var _model = acmContext.StoreContracts.Where(e => e.Id == model.id).FirstOrDefault();
                if (_model != null)
                {
                    _model.CheckInContract = model.CheckInContract;
                    _model.CheckOutContract = model.CheckOutContract;
                    _model.CreatedOn = DateTime.Now;
                    _model.StoreId = model.storeId;
                    acmContext.SaveChanges();
                    response.Status = true;
                }
                else
                {
                    StoreContracts storeModel = new StoreContracts();
                    storeModel.CheckInContract = model.CheckInContract;
                    storeModel.CheckOutContract = model.CheckOutContract;
                    storeModel.CreatedOn = DateTime.Now;
                    storeModel.StoreId = model.storeId;
                    acmContext.Add(storeModel);
                    acmContext.SaveChanges();
                    response.Data = null;
                    response.Status = true;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public ResponseModel<ManageContractViewModel> EditContract(string id)
        {
            ResponseModel<ManageContractViewModel> response = new ResponseModel<ManageContractViewModel> { Data = new ManageContractViewModel() };
            try
            {
              
                var model = acmContext.StoreContracts.Where(e => e.StoreId == id).FirstOrDefault();

                if (model != null)
                {
                    response.Data.CheckInContract = model.CheckInContract;
                    response.Data.CheckOutContract = model.CheckOutContract;
                    response.Data.storeId = model.StoreId;
                    response.Status = true;
                }
                else
                {                  
                    response.Data = null;
                    response.Status = true;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public ResponseModel<StoreInfoViewModel> WebStoreLogo(string userId)
        {
            ResponseModel<StoreInfoViewModel> response = new ResponseModel<StoreInfoViewModel> { Data = new StoreInfoViewModel() };
            try
            {
                var model = acmContext.StoreInfo.Where(e => e.StoreId == userId).FirstOrDefault();
                if (model != null)
                {
                    response.Data.Logo = model.Logo;
                    response.Data.LogoId = model.LogoId;
                    response.Data.StoreId = model.StoreId == null ? userId : model.StoreId;
                    response.Data.LogoName = model.Logo;
                    // response.Data = list;
                    response.Status = true;
                }
                else
                {

                    //response.Data.StoreId = model.StoreId == null ? userId : model.StoreId;
                    // response.Data = list;
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
        public CheckInForm HistoryDetail(int Id)
        {
            var result = acmContext.CheckInForm.Include("ProfileInfo").Include("Gallery").Where(k => k.Id == Id).FirstOrDefault();
            return result;
        }
    }
}
