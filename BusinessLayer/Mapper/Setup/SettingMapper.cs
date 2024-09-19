using System.Collections.Generic;
using System.Linq;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Mapper
{
    public class SettingMapper
    {
        public static List<SettingDTO> GetAllSettingDTO(List<Setting> SettingList)
        {

            var SettingDTOList = SettingList.Select(x => new SettingDTO
            {
                SettingName = x.SettingName,
                SettingValue = x.SettingValue,
                Description = x.Description,
                SettingType = x.SettingType
            }).ToList();

            return SettingDTOList;
        }

        public static Setting GetSettingDAO(SettingDTO SettingDTO)
        {

            return new Setting()
            {
                SettingName = SettingDTO.SettingName,
                SettingValue = SettingDTO.SettingValue,
                Description = SettingDTO.Description,
                SettingType = SettingDTO.SettingType
            };          
        }

        public static SettingDTO GetSettingDTO(Setting SettingModel)
        {

            return new SettingDTO()
            {
                SettingName = SettingModel.SettingName,
                SettingValue = SettingModel.SettingValue,
                Description = SettingModel.Description,
                SettingType = SettingModel.SettingType
            };
        }
    }
}
