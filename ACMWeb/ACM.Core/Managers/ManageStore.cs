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
    }
}
