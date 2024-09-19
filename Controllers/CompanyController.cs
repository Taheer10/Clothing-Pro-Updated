using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.Models;

namespace ClothingPro.Web.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _CompanyService;

        public CompanyController(ICompanyService CompanyService)
        {
            _CompanyService = CompanyService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var result = _CompanyService.GetAllCompany();
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
            CompanyDTO model = new CompanyDTO();

            if (mnId > 0)
            {
                model = _CompanyService.GetCompanyByIdd(mnId);
            }
            else
            {

            }

            return View("Create", model);
        }

        [HttpPost]
        public IActionResult CreatePost(CompanyDTO model)
        {
            try
            {


                if (ModelState.IsValid)
                {
                 
                    int StId = _CompanyService.CreateCompany(model);
                    return Json("success");
                }


                return View("Create", model);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult UpdateCompany(CompanyDTO model)
        {
            try
            {
                _CompanyService.UpdateCompany(model);
                return Json("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Delete(int mnId)
        {
            try
            {
                var data = _CompanyService.DeleteCompany(mnId);
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