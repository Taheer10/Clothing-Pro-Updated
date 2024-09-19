using System.Collections.Generic;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.Models;

namespace ClothingPro.BusinessLayer.Interface
{
    public interface ICompanyService
    {
        List<CompanyDTO> GetAllCompany();
        string GetValueByName(string CompanyName);
        void UpdateCompany(CompanyDTO CompanyUi);
        CompanyDTO GetCompanyById(string CompanyName);
        bool DeleteCompany(int mnId);
        CompanyDTO GetCompanyByIdd(int MnId);
        int CreateCompany(CompanyDTO CompanyUI);
    }
}