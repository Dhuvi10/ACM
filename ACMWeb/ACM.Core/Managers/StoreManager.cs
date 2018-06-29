using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.CheckInContractViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ACM.Core.Managers
{
    public class StoreManager : IStoreManager
    {
        private ACMContext acmContext;

        public StoreManager(ACMContext _acmContext)
        {
            acmContext = _acmContext;
        }
        public ResponseModel<List<StoreLogo>> StoreLogoList()
        {
            ResponseModel<List<StoreLogo>> response = new ResponseModel<List<StoreLogo>> { Data = new List<StoreLogo>() };
            try
            {
                var model = acmContext.StoreInfo.Select(x => new StoreLogo { StoreId = x.StoerId, Logo = x.Logo }).ToList();
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
        public ResponseModel<StoreInfoViewModel> StoreLogo(string userId)
        {
            ResponseModel<StoreInfoViewModel> response = new ResponseModel<StoreInfoViewModel> { Data = new StoreInfoViewModel() };
            try
            {
                var model = acmContext.StoreInfo.Where(e => e.StoerId == userId).FirstOrDefault();
                if (model != null)
                {
                    response.Data.Logo = model.Logo;
                    response.Data.LogoId = model.LogoId;
                    response.Data.StoerId = model.StoerId==null? userId: model.StoerId;
                    response.Data.LogoName = model.Logo;
                    // response.Data = list;
                    response.Status = true;
                }
                else
                {
               
                    //response.Data.StoerId = model.StoerId == null ? userId : model.StoerId;
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
                var model = acmContext.StoreInfo.Where(e => e.StoerId == _model.StoerId).FirstOrDefault();
                if (model != null)
                {
                    model.Logo = _model.LogoName;
                   // model.LogoId = _model.LogoId;
                    model.StoerId = _model.StoerId;

                    acmContext.SaveChanges();

                    // response.Data = list;
                    response.Status = true;
                }
                else
                {
                    StoreInfo storeModel = new StoreInfo();
                    storeModel.Logo = _model.LogoName;
                    //storeModel.LogoId = model.LogoId;
                    storeModel.StoerId = _model.StoerId;
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

        public ResponseModel<CheckInContractsViewModel> SaveCheckInContract(CheckInContractsViewModel _model)
        {
            ResponseModel<CheckInContractsViewModel> response = new ResponseModel<CheckInContractsViewModel> { Data = new CheckInContractsViewModel() };
            try
            {
                var model = acmContext.CheckInForm.Where(e => e.Id == _model.Id).FirstOrDefault();
                if (model != null)
                {
                    model.Id = _model.Id;
                    model.SummeryOfTaskCompleted = _model.SummeryOfTaskCompleted;
                    model.EmailAddress = _model.EmailAddress;
                    model.Name = _model.Name;
                    model.Vin = _model.Vin;
                    model.Year = _model.Year;
                    model.PartsNeeded = _model.PartsNeeded;
                    model.PersonalItemInVehicle = _model.PersonalItemInVehicle;
                    model.PhoneNumber = _model.PhoneNumber;
                    model.OdoMeter = _model.OdoMeter;
                    model.Model = _model.Model;
                    model.Make = _model.Make;
                    model.CreatedOn = DateTime.Now;
                    model.IsActive = _model.IsActive;
                    model.StoreId = _model.StoreId;
                    acmContext.SaveChanges();

                    // response.Data = list;
                    response.Message = "Record updated successfully";
                    response.Status = true;
                }
                else
                {
                    CheckInForm modelCheckInForm = new CheckInForm();
                    modelCheckInForm.SummeryOfTaskCompleted = _model.SummeryOfTaskCompleted;
                    modelCheckInForm.EmailAddress = _model.EmailAddress;
                    modelCheckInForm.Name = _model.Name;
                    modelCheckInForm.Vin = _model.Vin;
                    modelCheckInForm.Year = _model.Year;
                    modelCheckInForm.PartsNeeded = _model.PartsNeeded;
                    modelCheckInForm.PersonalItemInVehicle = _model.PersonalItemInVehicle;
                    modelCheckInForm.PhoneNumber = _model.PhoneNumber;
                    modelCheckInForm.OdoMeter = _model.OdoMeter;
                    modelCheckInForm.Model = _model.Model;
                    modelCheckInForm.Make = _model.Make;
                    modelCheckInForm.CreatedOn = DateTime.Now;
                    modelCheckInForm.IsActive = _model.IsActive;
                    modelCheckInForm.StoreId = _model.StoreId;
                    acmContext.Add(modelCheckInForm);
                    acmContext.SaveChanges();
                    response.Data = null;
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

        public ResponseModel<List<CheckInContractsViewModel>> ManageContractList()
        {
            ResponseModel<List<CheckInContractsViewModel>> response = new ResponseModel<List<CheckInContractsViewModel>> { Data = new List<CheckInContractsViewModel>()};
            var _list = acmContext.CheckInForm.Select(e => e).ToList();
            foreach (var item in _list)
            {
               // ManageStoreContractsViewModel model = new ManageStoreContractsViewModel();
               // model.Id = item.Id;
               // model.SummeryOfTaskCompleted = item.SummeryOfTaskCompleted;
               // model.EmailAddress = item.EmailAddress;
               // model.Name = item.Name;
               // model.Vin = item.Vin;
               // model.Year = item.Year;
               // model.PartsNeeded = item.PartsNeeded;
               // model.PersonalItemInVehicle = item.PersonalItemInVehicle;
               // model.PhoneNumber = item.PhoneNumber;
               // model.OdoMeter = item.OdoMeter;
               // model.Model = item.Model;
               // model.Make = item.Make;
               //// model.CreatedOn = DateTime.Now;
               // model.IsActive = item.IsActive;
               // response.Data.Add(model);
               // response.Status = true;
            }
            return response;
        }

    }
}
