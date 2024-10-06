using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Helper;
using ClothingPro.BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.Models;

namespace ClothingPro.Web.Controllers
{
    // [SessionTimeout]
    //[Authorize]
    public class StockController : BaseController
    {
        private readonly IStockService _stockService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMenuHeaderService _menuHeaderService;
        private readonly IColorImagesService _colorImagesService;

        public StockController(IStockService stockService, IWebHostEnvironment webHostEnvironment, IMenuHeaderService menuHeaderService, IColorImagesService colorImagesService)
        {
            _stockService = stockService;
            _webHostEnvironment = webHostEnvironment;
            _menuHeaderService = menuHeaderService;
            _colorImagesService = colorImagesService;
        }
        public IActionResult Index(int inActive = 0)
        {
            var result = _stockService.GetAllStockBYFilter(inActive);
            ViewBag.InActive = inActive;
            return View(result);
        }

        public IActionResult StockPopularItems()
        {
            var result = _stockService.GetAllKindOfStock();
            return View("StockPopularItems", result.stockPopularList);
        }

        public IActionResult StockPantsItems()
        {
            var result = _stockService.GetAllKindOfStock();
            return View(result.stockPantsList);
        }

        [Route("stock/create-stock")]
        [HttpGet]
        public IActionResult Create(int StId)
        {
            StockDTO model = new StockDTO();

            if (StId > 0)
            {
                model = _stockService.GetStockById(StId);
                model.ColorImagesDTOs = _colorImagesService.GetAllColorImagesListByStockId(StId);

            }
            else
            {

            }
            if (model.StFlagVal == null)
            {
                model.StFlagVal = 0;
            }
            ViewBag.MenuHeaderList = (new MVCHelper()).BindDropdownList("MENUHEADER", false);
            return View("Create", model);
        }


        [HttpGet]
        public IActionResult StockDetail(int StId)
        {
            StockDTO model = new StockDTO();
            var menuheaderList = _menuHeaderService.GetAllMenuHeader();

            if (StId > 0)
            {
                model = _stockService.GetStockById(StId);
                model.MenuHeaderLists = menuheaderList;
                model.ColorImagesDTOs = _colorImagesService.GetAllColorImagesListByStockId(StId);
            }
            else
            {

            }
            //  ViewBag.MenuHeaderList = (new MVCHelper()).BindDropdownList("MENUHEADER", false);
            return View("StockDetail", model);
        }

        //[Route("stock/post-stock")]
        //[HttpPost]
        //public IActionResult CreatePost(StockDTO model)
        //{
        //    try 
        //     {
        //        //if (new MVCHelper().CHECK_DUPLICATE("Stock", "StId", model.StId.ToString(), "StName", model.StName))
        //        //{
        //        //    //ModelState.AddModelError("duplicate", "Stock already exists.");
        //        //    return Json("duplicate", "Stock already exists.");
        //        //}

        //        if (ModelState.IsValid)
        //        {
        //            if (model.StId != 0)
        //            {
        //                var stockdetail = _stockService.GetStockById(model.StId);
        //                model.StImage = stockdetail.StImage;
        //                model.StAddedDate = stockdetail.StAddedDate;
        //            }
        //            else
        //            {
        //                model.StAddedDate = DateTime.Now;
        //            }

        //            int StId = _stockService.CreateStock(model);                 
        //            return Json("success");
        //        }


        //        return View("Create", model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}
        [Route("stock/post-stock")]
        [HttpPost]
        public async Task<IActionResult> CreatePost()
        {
            try
            {
                var form = Request.Form;

                // Extract and deserialize model
                var stockInfo = form["model"].ToString();
                var model = JsonConvert.DeserializeObject<StockDTO>(stockInfo);
                var colors = form["StColours"].ToString();
                model.StColour = colors;
                // Extract files
                var stImages = Request.Form.Files;
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (ModelState.IsValid)
                {
                    var imagePaths = new List<string>();
                    var imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    foreach (var image in stImages)
                    {
                        var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest($"File type not supported: {image.FileName}");
                        }
                        if (image != null && image.Length > 0)
                        {
                            var filePath = Path.Combine(imagesDirectory, Path.GetFileName(image.FileName));
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                            imagePaths.Add("/images/" + Path.GetFileName(image.FileName));
                        }
                    }

                    model.StImage = string.Join(";", imagePaths);
                    model.StAddedDate = model.StId != 0 ? _stockService.GetStockById(model.StId).StAddedDate : DateTime.Now;
                    if (stImages.Count == 0)
                    {
                        var stockdetail = _stockService.GetStockById(model.StId);
                        if (!string.IsNullOrEmpty(stockdetail.StImage))
                        {
                            model.StImage = stockdetail.StImage;
                        }
                    }

                    _stockService.CreateStock(model);
                    return Json("success");
                }

                return Json("ModelState is not valid.");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }



        //[Route("stock/delete-stock")]
        [HttpPost]
        public IActionResult Delete(int StId)
        {
            try
            {
                var data = _stockService.DeleteStock(StId);
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

        [HttpPost]
        public IActionResult UpdateFlag(int stockId, int FlagVal)
        {
            try
            {
                if (stockId != 0)
                {
                    var data = _stockService.GetStockById(stockId);
                    data.StFlagVal = FlagVal;
                    var stockid = _stockService.CreateStock(data);
                    //return this.Ok(data);
                    if (stockid != 0)
                    {
                        if (FlagVal == 0)
                        {
                            return Json(new { success = true, message = "Data Updated Successfully to Normal Color " });
                        }
                        else if (FlagVal == 1)
                        {
                            return Json(new { success = true, message = "Data Updated Successfully To ColorList " });
                        }
                    }
                    else // If deletion failed for some reason
                    {
                        return Json(new { success = false, message = "Failed to Update the data" });

                    }
                }
                return Json(new { success = false, message = "Stock Not Selected" });

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        //[HttpGet]
        //public JsonResult Search(string search)
        //{
        //    var model = _stockService.GetStockMenuList(search);

        //    return Json(new { model = JsonConvert.SerializeObject(model) });
        //} 


        [HttpGet]
        public JsonResult StockJsonReturn(int StId)
        {
            var model = _stockService.GetStockById(StId);

            return Json(new { model = JsonConvert.SerializeObject(model) });
        }


    }
}







