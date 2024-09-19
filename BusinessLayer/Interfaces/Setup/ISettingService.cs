using System.Collections.Generic;
using ClothingPro.BusinessLayer.DTO;

namespace ClothingPro.BusinessLayer.Interface
{
    public interface ISettingService
    {
        List<SettingDTO> GetAllSetting();
        string GetValueByName(string settingName);
        void UpdateSetting(SettingDTO settingUi);
        SettingDTO GetSettingById(string settingName);
    }
}