using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.Models;

namespace ClothingPro.Web.Controllers
{
    public class ColorImagesController : BaseController
    {
        private readonly IColorImagesService _ColorImagesService;
        private readonly IStockService _StockService;

        public ColorImagesController(IColorImagesService ColorImagesService, IStockService stockService)
        {
            _ColorImagesService = ColorImagesService;
            _StockService = stockService;
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


        [HttpGet]
        public IActionResult Create(int mnId)
        {
            ColorImagesDTO model = new ColorImagesDTO();

            if (mnId > 0)
            {
                model = _ColorImagesService.GetColorImagesByIdd(mnId);
            }
            else
            {

            }

            return View("Create", model);
        }

        [HttpPost]
        public IActionResult CreatePost(ColorImagesDTO model)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    int StId = _ColorImagesService.CreateColorImages(model);
                    return Json("success");
                }


                return View("Create", model);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

      

        public IActionResult Delete(int mnId)
        {
            try
            {
                var data = _ColorImagesService.DeleteColorImages(mnId);
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