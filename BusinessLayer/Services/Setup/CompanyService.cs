using Mapster;
using System;
using System.Collections.Generic;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.Mapper;
using ClothingPro.BusinessLayer.Repository;
using ClothingPro.BusinessLayer.Helper;
using ClothingPro.DataAccessLayer.DbAccess;
using Microsoft.Data.SqlClient;
using System.Data;
using ClothingPro.Models;

namespace ClothingPro.BusinessLayer.BusinessService
{
    public class CompanyService : ICompanyService
    {
        private readonly UnitOfWork _unitOfWork;

        public CompanyService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<CompanyDTO> GetAllCompany()
        {
            try
            {
                using (CompanyRepository CompanyRepo = new CompanyRepository())
                {
                    var CompanyDAOList = CompanyRepo.CompanyList();

                    var CompanyDTOList = CompanyMapper.GetAllCompanyDTO(CompanyDAOList);
                    return CompanyDTOList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    

        public CompanyDTO GetCompanyByIdd(int MnId)
        {
            try
            {
                CompanyRepository Companyrepo = new CompanyRepository();
                var Company = Companyrepo.GetCompanyByIdd(MnId);
                if (MnId > 0)
                {
                    return CompanyMapper.GetCompanyDTO(Company);
                }
                else
                {
                    return new CompanyDTO();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CompanyDTO GetCompanyById(string CompanyName)
        {
            try
            {
                using (CompanyRepository CompanyRepo = new CompanyRepository())
                {
                    var CompanyDAO = CompanyRepo.GetCompanyById(CompanyName);
                    var CompanyDTOMOdel = CompanyMapper.GetCompanyDTO(CompanyDAO);

                    return CompanyDTOMOdel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CreateCompany(CompanyDTO CompanyUI)
        {
            try
            {
                using (CompanyRepository CompanyRepo = new CompanyRepository())
                {
                    var CompanyModel = CompanyMapper.GetCompanyDAO(CompanyUI);
                    if (CompanyModel.CompanyId > 0)
                    {
                        var stockDAO = CompanyRepo.GetCompanyByIdd(CompanyUI.CompanyId);
                        CompanyModel.CompanyId = stockDAO.CompanyId;

                        CompanyModel = CompanyModel.Adapt(stockDAO);
                        var result = CompanyRepo.UpdateCompany(CompanyModel);
                        return CompanyUI.CompanyId;
                    }
                    else
                    {
                        return CompanyRepo.CreateCompany(CompanyModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetValueByName(string CompanyName)
        {
            try
            {
                using (CompanyRepository CompanyRepo = new CompanyRepository())
                {
                    return CompanyRepo.GetCompanyById(CompanyName).CompanyName;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool DeleteCompany(int mnId)
        {
            try
            {

                using (CompanyRepository menuRepo = new CompanyRepository())
                {

                    var result = menuRepo.DeleteCompany(mnId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCompany(CompanyDTO CompanyDTO)
        {
            try
            {
                using (CompanyRepository CompanyRepo = new CompanyRepository())
                {
                    var CompanyDAOModel = CompanyRepo.GetCompanyById(CompanyDTO.CompanyName);

                    var CompanyModel = CompanyMapper.GetCompanyDAO(CompanyDTO);

                    CompanyModel.CompanyName = CompanyDAOModel.CompanyName;

                    CompanyModel = CompanyModel.Adapt(CompanyDAOModel);

                    CompanyRepo.UpdateCompany(CompanyModel);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}