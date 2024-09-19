using System;
using System.Collections.Generic;
using System.Linq;
using ClothingPro.DataAccessLayer.DbAccess;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Repository
{
    public class CompanyRepository : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public CompanyRepository()
        {
            _unitOfWork = new UnitOfWork();
        }
        public List<Company> CompanyList()
        {
            try
            {

                var result = _unitOfWork.CompanyRepository.GetAll().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Company GetCompanyById(string CompanyName)
        {
            try
            {
                if (CompanyName != null)
                {
                    var result = _unitOfWork.CompanyRepository.GetBy(x => x.CompanyName == CompanyName).FirstOrDefault();
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateCompany(Company model)
        {
            try
            {

                var result = _unitOfWork.CompanyRepository.Add(model).CompanyId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Company GetCompanyByIdd(int MnId)
        {
            try
            {
                if (MnId != 0)
                {
                    var result = _unitOfWork.CompanyRepository.FindBy(x => x.CompanyId == MnId);
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCompany(int mnId)
        {
            try
            {
                var menuModel = _unitOfWork.CompanyRepository.FindBy(x => x.CompanyId == mnId);

                _unitOfWork.CompanyRepository.Delete(menuModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCompany(Company model)
        {
            try
            {
                 _unitOfWork.CompanyRepository.Update(model);
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
