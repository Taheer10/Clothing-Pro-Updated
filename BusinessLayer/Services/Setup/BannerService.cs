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
    public class BannerService : IBannerService
    {
        private readonly UnitOfWork _unitOfWork;

        public BannerService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<BannerDTO> GetAllBanner()
        {
            try
            {
                using (BannerRepository BannerRepo = new BannerRepository())
                {
                    var BannerDAOList = BannerRepo.BannerList();

                    var BannerDTOList = BannerMapper.GetAllBannerDTO(BannerDAOList).OrderBy(x=>x.BannerSortOrder).ToList();
                    return BannerDTOList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        public BannerDTO GetBannerByIdd(int MnId)
        {
            try
            {
                BannerRepository Bannerrepo = new BannerRepository();
                var Banner = Bannerrepo.GetBannerByIdd(MnId);
                if (MnId > 0)
                {
                    return BannerMapper.GetBannerDTO(Banner);
                }
                else
                {
                    return new BannerDTO();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BannerDTO GetBannerById(string BannerName)
        {
            try
            {
                using (BannerRepository BannerRepo = new BannerRepository())
                {
                    var BannerDAO = BannerRepo.GetBannerById(BannerName);
                    var BannerDTOMOdel = BannerMapper.GetBannerDTO(BannerDAO);

                    return BannerDTOMOdel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CreateBanner(BannerDTO BannerUI)
        {
            try
            {
                using (BannerRepository BannerRepo = new BannerRepository())
                {
                    var BannerModel = BannerMapper.GetBannerDAO(BannerUI);
                    if (BannerModel.BannerId > 0)
                    {
                        var stockDAO = BannerRepo.GetBannerByIdd(BannerUI.BannerId);
                        BannerModel.BannerId = stockDAO.BannerId;

                        BannerModel = BannerModel.Adapt(stockDAO);
                        var result = BannerRepo.UpdateBanner(BannerModel);
                        return BannerUI.BannerId;
                    }
                    else
                    {
                        return BannerRepo.CreateBanner(BannerModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetValueByName(string BannerName)
        {
            try
            {
                using (BannerRepository BannerRepo = new BannerRepository())
                {
                    return BannerRepo.GetBannerById(BannerName).BannerImg;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool DeleteBanner(int mnId)
        {
            try
            {

                using (BannerRepository menuRepo = new BannerRepository())
                {

                    var result = menuRepo.DeleteBanner(mnId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

    }

}