using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClothingPro.BusinessLayer.Helper;

namespace ClothingPro.Web.Controllers
{
    public class ColorImagesController : BaseController
    {
        private readonly IColorImagesService _ColorImagesService;
        private readonly IStockService _StockService;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ColorImagesController(IColorImagesService ColorImagesService, IStockService stockService, IWebHostEnvironment webHostEnvironment)
        {
            _ColorImagesService = ColorImagesService;
            _StockService = stockService;
            _WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var result = _ColorImagesService.GetAllColorImages();
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //[HttpGet]
        //public IActionResult CreateInfo(int ClrinfoId, List<ColorImagesDTO> colorImagesList)
        //{
        //    ColorImagesDTO model = new ColorImagesDTO();

        //    if (ClrinfoId > 0)
        //    {
        //        model = _ColorImagesService.GetColorImagesByIdd(ClrinfoId);
        //        model.colorImagesList = colorImagesList;
        //    }
        //    else
        //    {

        //    }

        //    return View("Create", model);

        //}


        [HttpGet]
        public IActionResult Create(int ClrId, int stkid = 0)
        {
            ColorImagesDTO model = new ColorImagesDTO();

            if (ClrId > 0)
            {
                model = _ColorImagesService.GetColorImagesByIdd(ClrId);
                model.colorImagesList = _ColorImagesService.GetAllColorImagesListByStockId(model.StockId);

                ViewBag.ColorImagesList = (new MVCHelper()).BindDropdownList("COLORIMAGES", false, "", "", model.StockId);
            }
            else
            {
                model.colorImagesList = _ColorImagesService.GetAllColorImagesListByStockId(stkid);
                model.StockDTO = _StockService.GetStockById(stkid);
                ViewBag.StockName = model.StockDTO.StName;
                ViewBag.StkId = stkid;

            }
            ViewBag.StockList = (new MVCHelper()).BindDropdownList("STOCKUpdate", true);
            return View("Create", model);
            //return Json(new { model = JsonConvert.SerializeObject(model) });
            //return Json(new { model });

        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(IFormFile ColorImagesListFile, List<ColorImagesDTO> ColorImagesList, int stockId, List<Int32> arrClrImgsIds, List<String> ColorNames)
        {
            try
            {
                if (stockId != 0)
                {
                    ColorImagesDTO colorImages = new ColorImagesDTO();
                    colorImages.colorImagesList = new List<ColorImagesDTO>();
                    var formfiles = Request.Form.Files;
                    List<Int32> ClrImgsIds = arrClrImgsIds;
                    List<String> ClrImgsNames = ColorNames;
                    List<ColorImagesDTO> clrImages = ColorImagesList;
                    //var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (formfiles.Count > 0)
                    {
                        int i;
                        for (i = 0; i < formfiles.Count; i++)
                        {
                            var file = formfiles[i];
                            string uploadsFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "colorimages");
                            if (!Directory.Exists(uploadsFolder))
                            {
                                Directory.CreateDirectory(uploadsFolder);
                            }
                            var fileName = formfiles[i].FileName;
                            var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                            string filePath = Path.Combine(uploadsFolder, fileName);

                            if (Directory.Exists(filePath))
                            {
                                return BadRequest($"File Already Exists: {fileName}");
                            }

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            colorImages.ColorImagesImg = "/colorimages/" + fileName;
                            if (ClrImgsIds.Count > 0)
                            {
                                int j;

                                for (j = 0; j < ClrImgsIds.Count; j++)
                                {
                                    if (i == j)
                                    {
                                        var colorImage = new ColorImagesDTO
                                        {
                                            ColorImagesImg = "/colorimages/" + fileName, // Set the image path,
                                            StockId = stockId,
                                            ColorImagesName = fileName,
                                            ColorImagesId = ClrImgsIds[j],
                                            //ColorName = FileNameWithoutExtension
                                        };
                                        int k;

                                        for (k = 0; k < ClrImgsNames.Count; k++)
                                        {
                                            if (k == j)
                                            {
                                                colorImage.ColorName = ClrImgsNames[k];
                                            }
                                        }
                                        colorImages.colorImagesList.Add(colorImage);
                                    }
                                }
                            }
                            else
                            {
                                int l;
                                for (l = 0; l < ClrImgsNames.Count; l++)
                                {
                                    if (i == l)
                                    {
                                        var colorImage = new ColorImagesDTO
                                        {
                                            ColorImagesImg = "/colorimages/" + fileName, // Set the image path,
                                            StockId = stockId,
                                            ColorImagesName = fileName,
                                            ColorName = ClrImgsNames[l]
                                        };
                                        colorImages.colorImagesList.Add(colorImage);
                                    }


                                }

                            }

                        }
                    }

                    await _ColorImagesService.CreateColorImagesList(colorImages.colorImagesList, stockId);
                    return Json("success");
                }

                return Json("Model state is invalid.");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }





        public IActionResult Delete(int clrId)
        {
            try
            {
                var data = _ColorImagesService.DeleteColorImages(clrId);
                //return this.Ok(data);
                if (data)
                {
                    return Json(new { success = true, message = "Data Deleted Successfully" });
                }
                else // If deletion failed for some reason
                {
                    return Json(new { success = false, message = "Failed to delete the data" });
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


    }
}