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
using ClothingPro.DataAccessLayer.Model;

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

        public List<ColorImagesDTO> GetAllColorImagesListByStockId(int stockId)
        {
            try
            {
                using (ColorImagesRepository ColorImagesRepo = new ColorImagesRepository())
                {
                    var ColorImagesDAOList = ColorImagesRepo.ColorImagesListBYStockId(stockId);

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
                if (MnId > 0 && ColorImages != null)
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

        public async Task<List<ColorImagesDTO>> CreateColorImagesList(List<ColorImagesDTO> ColorImagesList, int StockId)
        {
            await InsertColorImagesDetail(ColorImagesList, StockId);
            return ColorImagesList;
        }

        public async Task<List<ColorImagesDTO>> InsertColorImagesDetail(List<ColorImagesDTO> model, int Id)
        {


            try
            {

                using (ColorImagesRepository colorImagesRepo = new ColorImagesRepository())
                {

                    foreach (var item in model)
                    {
                        item.StockId = Id;
                    }

                    var ColorImagesDetailModel = await ColorImagesDetailList(model.ToList());


                    var createColorImagesDetail = ColorImagesDetailModel.Where(x => x.ColorImagesId == 0);
                    var updateColorImagesDetail = ColorImagesDetailModel.Where(x => x.ColorImagesId != 0);
                    ColorImagesDTO clr = new ColorImagesDTO();

                    if (createColorImagesDetail.Count() > 0)
                    {


                        //await unitOfWork.SaveChangesAsync()
                        //    .ConfigureAwait(false);
                        var saveCreateColorList = colorImagesRepo.AddColorImagesRange(createColorImagesDetail.ToList());
                        return clr.colorImagesList;
                        
                    }

                    if (updateColorImagesDetail.Count() > 0)
                    {
                        var saveUpdateColorList = colorImagesRepo.UpdateColorImagesRange(updateColorImagesDetail.ToList());
                        return clr.colorImagesList;

                    }

                    return model.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<ColorImages>> ColorImagesDetailList(List<ColorImagesDTO> ColorImagesDetailList)
        {
            var ColorImagesDetaillList = ColorImagesDetailList.Select(x => new ColorImages
            {
                ColorImagesId = x.ColorImagesId,
                ColorImagesImg = x.ColorImagesImg,
                ColorImagesName = x.ColorImagesName,
                StockId = x.StockId,
                ColorName = x.ColorName
            }).ToList();

            return await Task.FromResult(ColorImagesDetaillList);
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

        public bool DeleteColorImages(int clrId)
        {
            try
            {

                using (ColorImagesRepository ColorImagesRepo = new ColorImagesRepository())
                {

                    var result = ColorImagesRepo.DeleteColorImages(clrId);
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