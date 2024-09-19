using System;
using System.Collections.Generic;
using System.Linq;
using ClothingPro.DataAccessLayer.DbAccess;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Repository
{
    public class SettingRepository : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public SettingRepository()
        {
            _unitOfWork = new UnitOfWork();
        }
        public List<Setting> SettingList()
        {
            try
            {

                var result = _unitOfWork.SettingRepository.GetAll().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Setting GetSettingById(string settingName)
        {
            try
            {
                if (settingName != null)
                {
                    var result = _unitOfWork.SettingRepository.GetBy(x => x.SettingName == settingName).FirstOrDefault();
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateSetting(Setting model)
        {
            try
            {
                 _unitOfWork.SettingRepository.Update(model);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
