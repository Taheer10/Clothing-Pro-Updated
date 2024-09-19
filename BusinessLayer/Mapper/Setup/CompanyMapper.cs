using System.Collections.Generic;
using System.Linq;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Mapper
{
    public class CompanyMapper
    {
        public static List<CompanyDTO> GetAllCompanyDTO(List<Company> CompanyList)
        {

            var CompanyDTOList = CompanyList.Select(x => new CompanyDTO
            {
                CompanyId = x.CompanyId,
                CompanyName = x.CompanyName,
                ContactNo = x.ContactNo,
                CompanyEmail = x.CompanyEmail,
            }).ToList();

            return CompanyDTOList;
        }

        public static Company GetCompanyDAO(CompanyDTO x)
        {

            return new Company()
            {
                CompanyId = x.CompanyId,
                CompanyName = x.CompanyName,
                ContactNo = x.ContactNo,
                CompanyEmail = x.CompanyEmail,
            };          
        }

        public static CompanyDTO GetCompanyDTO(Company x)
        {

            return new CompanyDTO()
            {
                CompanyId = x.CompanyId,
                CompanyName = x.CompanyName,
                ContactNo = x.ContactNo,
                CompanyEmail = x.CompanyEmail,
            };
        }
    }
}
