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
    public class ColorImagesService : IColorImagesService
    {
        private readonly UnitOfWork _unitOfWork;

        public ColorImagesService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<ColorImagesDTO> GetAllColorImages()
        {
            try
            {
                using (ColorImagesRepository ColorImagesRepo = new ColorImagesRepository())
                {
                    var ColorImagesDAOList = ColorImagesRepo.ColorImagesList();

                    var ColorImagesDTOList = ColorImagesMapper.GetAllColorImagesDTO(ColorImagesDAOList).ToList();
                    return ColorImagesDTOList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ColorImagesDTO GetColorImagesByIdd(int MnId)
        {
            try
            {
                ColorImagesRepository ColorImagesrepo = new ColorImagesRepository();
                var ColorImages = ColorImagesrepo.GetColorImagesByIdd(MnId);
                if (MnId > 0)
                {
                    return ColorImagesMapper.GetColorImagesDTO(ColorImages);
                }
                else
                {
                    return new ColorImagesDTO();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ColorImagesDTO GetColorImagesById(string ColorImagesName)
        {
            try
            {
                using (ColorImagesRepository ColorImagesRepo = new ColorImagesRepository())
                {
                    var ColorImagesDAO = ColorImagesRepo.GetColorImagesById(ColorImagesName);
                    var ColorImagesDTOMOdel = ColorImagesMapper.GetColorImagesDTO(ColorImagesDAO);

                    return ColorImagesDTOMOdel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CreateColorImages(ColorImagesDTO ColorImagesUI)
        {
            try
            {
                using (ColorImagesRepository ColorImagesRepo = new ColorImagesRepository())
                {
                    var ColorImagesModel = ColorImagesMapper.GetColorImagesDAO(ColorImagesUI);
                    if (ColorImagesModel.ColorImagesId > 0)
                    {
                        var stockDAO = ColorImagesRepo.GetColorImagesByIdd(ColorImagesUI.ColorImagesId);
                        ColorImagesModel.ColorImagesId = stockDAO.ColorImagesId;

                        ColorImagesModel = ColorImagesModel.Adapt(stockDAO);
                        var result = ColorImagesRepo.UpdateColorImages(ColorImagesModel);
                        return ColorImagesUI.ColorImagesId;
                    }
                    else
                    {
                        return ColorImagesRepo.CreateColorImages(ColorImagesModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetValueByName(string ColorImagesName)
        {
            try
            {
                using (ColorImagesRepository ColorImagesRepo = new ColorImagesRepository())
                {
                    return ColorImagesRepo.GetColorImagesById(ColorImagesName).ColorImagesName;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool DeleteColorImages(int mnId)
        {
            try
            {

                using (ColorImagesRepository menuRepo = new ColorImagesRepository())
                {

                    var result = menuRepo.DeleteColorImages(mnId);
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