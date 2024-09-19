using System;
using System.Collections.Generic;
using System.Linq;
using ClothingPro.DataAccessLayer.DbAccess;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Repository
{
    public class MenuHeaderRepository : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public MenuHeaderRepository()
        {
            _unitOfWork = new UnitOfWork();
        }
        public List<MenuHeader> MenuHeaderList()
        {
            try
            {

                var result = _unitOfWork.MenuHeaderRepository.GetAll().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuHeader GetMenuHeaderById(string MenuHeaderName)
        {
            try
            {
                if (MenuHeaderName != null)
                {
                    var result = _unitOfWork.MenuHeaderRepository.GetBy(x => x.MenuHeaderName == MenuHeaderName).FirstOrDefault();
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateMenuHeader(MenuHeader model)
        {
            try
            {

                var result = _unitOfWork.MenuHeaderRepository.Add(model).MenuHeaderId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuHeader GetMenuHeaderByIdd(int MnId)
        {
            try
            {
                if (MnId != 0)
                {
                    var result = _unitOfWork.MenuHeaderRepository.FindBy(x => x.MenuHeaderId == MnId);
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMenuHeader(int mnId)
        {
            try
            {
                var menuModel = _unitOfWork.MenuHeaderRepository.FindBy(x => x.MenuHeaderId == mnId);

                _unitOfWork.MenuHeaderRepository.Delete(menuModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMenuHeader(MenuHeader model)
        {
            try
            {
                 _unitOfWork.MenuHeaderRepository.Update(model);
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
