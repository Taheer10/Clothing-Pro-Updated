using System;
using System.Collections.Generic;
using System.Linq;
using ClothingPro.DataAccessLayer.DbAccess;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Repository
{
    public class BannerRepository : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public BannerRepository()
        {
            _unitOfWork = new UnitOfWork();
        }
        public List<Banner> BannerList()
        {
            try
            {

                var result = _unitOfWork.BannerRepository.GetAll().Where(x=>x.BannerIsActive == 0).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Banner GetBannerById(string BannerImg)
        {
            try
            {
                if (BannerImg != null)
                {
                    var result = _unitOfWork.BannerRepository.GetBy(x => x.BannerImg == BannerImg).FirstOrDefault();
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateBanner(Banner model)
        {
            try
            {

                var result = _unitOfWork.BannerRepository.Add(model).BannerId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Banner GetBannerByIdd(int MnId)
        {
            try
            {
                if (MnId != 0)
                {
                    var result = _unitOfWork.BannerRepository.FindBy(x => x.BannerId == MnId);
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBanner(int mnId)
        {
            try
            {
                var menuModel = _unitOfWork.BannerRepository.FindBy(x => x.BannerId == mnId);

                _unitOfWork.BannerRepository.Delete(menuModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateBanner(Banner model)
        {
            try
            {
                 _unitOfWork.BannerRepository.Update(model);
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
