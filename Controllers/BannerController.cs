using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.Models;
using ClothingPro.DataAccessLayer.Model;
using Newtonsoft.Json;
using System.Linq;

namespace ClothingPro.Web.Controllers
{
    public class BannerController : BaseController
    {
        private readonly IBannerService _BannerService;
        private readonly IStockService _StockService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannerController(IBannerService BannerService, IStockService stockService, IWebHostEnvironment webHostEnvironment)
        {
            _BannerService = BannerService;
            _StockService = stockService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var result = _BannerService.GetAllBanner();
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        public IActionResult Create(int bnId)
        {
            BannerDTO model = new BannerDTO();

            if (bnId > 0)
            {
                model = _BannerService.GetBannerByIdd(bnId);
            }
            else
            {

            }

            return View("Create", model);
        }

        //[HttpPost]
        //public IActionResult CreatePost(BannerDTO model)
        //{
        //    try
        //    {


        //        if (ModelState.IsValid)
        //        {

        //            int StId = _BannerService.CreateBanner(model);
        //            return Json("success");
        //        }


        //        return View("Create", model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> CreatePost(IFormFile BannerImg, int? BannerIsActive, int? StSortOrder)
        {
            var form = Request.Form;

            var bannerInfo = form["model"].ToString();
            var model = JsonConvert.DeserializeObject<BannerDTO>(bannerInfo);
            BannerDTO bannerdetail = new BannerDTO();
            if (BannerImg == null)
            {
                bannerdetail = _BannerService.GetBannerByIdd(model.BannerId);
                model.BannerImg = bannerdetail.BannerImg;
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (model.BannerId >= 0)
            {
                //if (BannerImg != null && BannerImg.Length > 0)
                //{
                if (model.BannerId == 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "banners");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string BannerName = "";
                    if (BannerImg != null)
                    {
                        BannerName = Path.Combine(uploadsFolder, BannerImg.FileName);

                    }
                    else if (bannerdetail != null)
                    {
                        BannerName = bannerdetail.BannerImg;
                    }
                    //if (!Directory.Exists(BannerName) && model.BannerId == 0)
                    //{
                    //    return BadRequest($"File {BannerName} Already Exists.");
                    //}
                    var fileExtension = Path.GetExtension(BannerName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest($"File type not supported: {BannerName}");
                    }
                    string filePath = Path.Combine(uploadsFolder, BannerName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await BannerImg.CopyToAsync(fileStream);
                    }
                    model.BannerImg = "/banners/" + BannerImg.FileName;
                }

                if(BannerImg != null)
                {
                    var fileExtension = Path.GetExtension(BannerImg.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest($"File type not supported: {BannerImg.FileName}");
                    }
                    model.BannerImg = "/banners/" + BannerImg.FileName;
                }


                _BannerService.CreateBanner(model);

                return Json(new { success = true, message = "Banner saved successfully!" });
                //}
            }


            return Json(new { success = false, message = "Failed to save banner!" });
        }



        public IActionResult Delete(int bnId)
        {
            try
            {
                var data = _BannerService.DeleteBanner(bnId);
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