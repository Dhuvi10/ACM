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
        public ResponseModel<StoreInfoViewModel> StoreLogo(string userId)
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

        public ResponseModel<CheckInContractsViewModel> SaveCheckInForm(CheckInContractsViewModel _model)
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
                    model.CustomerPartSupplied = _model.CustomerPartSupplied;
                    model.OdoMeter = _model.OdoMeter;
                    model.Models = _model.Models;
                    model.Make = _model.Make;
                    model.CreatedOn = DateTime.Now;
                    model.IsActive = _model.IsActive;
                    model.IsCheckOut = false;
                    model.StoreId = _model.StoreId;
                    acmContext.SaveChanges();

                    // response.Data = list;
                    response.Data = _model;
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
                    modelCheckInForm.Models = _model.Models;
                    modelCheckInForm.Make = _model.Make;
                    modelCheckInForm.CustomerPartSupplied = _model.CustomerPartSupplied;
                    modelCheckInForm.CreatedOn = DateTime.Now;
                    modelCheckInForm.IsActive = _model.IsActive;
                    modelCheckInForm.StoreId = _model.StoreId;
                    modelCheckInForm.IsCheckOut = false;
                    acmContext.Add(modelCheckInForm);
                    acmContext.SaveChanges();
                    _model.Id = modelCheckInForm.Id;
                    _model.StoreId = modelCheckInForm.StoreId;
                    response.Data = _model;
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
        public ResponseModel<List<CheckInContractsViewModel>> ManageContractList(bool isCheckOut)
        {
            ResponseModel<List<CheckInContractsViewModel>> response = new ResponseModel<List<CheckInContractsViewModel>> { Data = new List<CheckInContractsViewModel>() };
            try
            {
                var _list = acmContext.CheckInForm.Where(x=>x.IsCheckOut== isCheckOut).Select(e => e).ToList();
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
                    model.IsActive = item.IsActive;
                    if (isCheckOut)
                    {
                        model.IsCheckOut = item.IsCheckOut;
                        model.CheckOutDate = item.CheckOutDate;
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
                var _list = acmContext.CheckInForm.Where(k=>k.StoreId==storeId).Select(e => e).ToList();
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
                    model.Data.StoreId = _model.StoreId;
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

    }
}
