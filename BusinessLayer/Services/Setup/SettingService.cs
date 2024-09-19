using Mapster;
using System;
using System.Collections.Generic;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.Mapper;
using ClothingPro.BusinessLayer.Repository;

namespace ClothingPro.BusinessLayer.BusinessService
{
    public class SettingService : ISettingService
    {
        public List<SettingDTO> GetAllSetting()
        {
            try
            {
                using (SettingRepository SettingRepo = new SettingRepository())
                {
                    var SettingDAOList = SettingRepo.SettingList();

                    var SettingDTOList = SettingMapper.GetAllSettingDTO(SettingDAOList);
                    return SettingDTOList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SettingDTO GetSettingById(string settingName)
        {
            try
            {
                using (SettingRepository SettingRepo = new SettingRepository())
                {
                    var SettingDAO = SettingRepo.GetSettingById(settingName);
                    var SettingDTOMOdel = SettingMapper.GetSettingDTO(SettingDAO);

                    return SettingDTOMOdel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetValueByName(string settingName)
        {
            try
            {
                using (SettingRepository SettingRepo = new SettingRepository())
                {
                    return SettingRepo.GetSettingById(settingName).SettingValue;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public void UpdateSetting(SettingDTO settingDTO)
        {
            try
            {
                using (SettingRepository SettingRepo = new SettingRepository())
                {
                    var SettingDAOModel = SettingRepo.GetSettingById(settingDTO.SettingName);

                    settingDTO.Description = SettingDAOModel.Description;
                    settingDTO.SettingType = SettingDAOModel.SettingType;

                    var SettingModel = SettingMapper.GetSettingDAO(settingDTO);

                    SettingModel.SettingName = SettingDAOModel.SettingName;

                    SettingModel = SettingModel.Adapt(SettingDAOModel);

                    SettingRepo.UpdateSetting(SettingModel);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}