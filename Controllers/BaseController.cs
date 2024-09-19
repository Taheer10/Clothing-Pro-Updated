using ClothingPro.BusinessLayer.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using ClothingPro.BusinessLayer.Helper;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.BusinessLayer.Repository;
//Added for git test

namespace ClothingPro.Web.Controllers
{
    //[Authorize]
    public abstract class BaseController : Controller
    {
        private const string sessionuserId = "_UserID";
        private const string sessionusergroup = "_UserGroup";
        private const string sessionbranchId = "_BranchID";
        //private readonly IStaffService _staffService;

        public BaseController()
        {
        }

        protected IActionResult Ok(object model = null)
        {
            if (model == null)
            {
                model = "OK";
            }

            this.Response.StatusCode = 200;

            string json = JsonConvert.SerializeObject(model);
            return this.Content(json, "application/json");
        }

        protected IActionResult Failed(string message, HttpStatusCode statusCode)
        {
            this.Response.StatusCode = (int)statusCode;
            return this.Content(message, MediaTypeNames.Text.Plain, Encoding.UTF8);
        }

        protected IActionResult InvalidModelState(ModelStateDictionary modelState)
        {
            string errors = string.Join("\n", this.ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage)));
            return this.Failed(errors, HttpStatusCode.BadRequest);
        }

        protected int GetStaffId()
        {
            return DBNullHandler.Int32(HttpContext.Session.GetString(sessionuserId), 0);
        }

        protected int GetBranchId()
        {
            return DBNullHandler.Int32(HttpContext.Session.GetString(sessionbranchId), 1);
        }

        protected int GetShiftId()
        {
            return 1;
        }

        protected string GetUserGroup()
        {
            return HttpContext.Session.GetString(sessionusergroup);
        }

        //public SelectList GetBranchList(bool blank = false, int branchId = 1)
        //{
        //    if (User.IsInRole("SUPER") || User.IsInRole("ADMIN"))
        //    {
        //        return (new MVCHelper()).BindDropdownList("BRANCH", true, "", "", branchId);
        //    }
        //    else
        //    {
        //        BranchRepository branchRepository = new BranchRepository();
        //        var branchmodel = branchRepository.GetBranchById(GetBranchId());

        //        List<SelectListItem> returnListItems = new List<SelectListItem>();
        //        returnListItems.Add(new SelectListItem
        //        {
        //            Text = branchmodel.Name,
        //            Value = branchmodel.BranchId.ToString(),
        //            Selected = true
        //        });

        //        return new SelectList(returnListItems, "Value", "Text");
        //        //return (new MVCHelper()).BindDropdownList("BRANCHRIGHT", blank, "", GetStaffId().ToString(), branchId);
        //    }
        //}

        //public SelectList GetDepartmentList(bool blank = false, string deptId = "", string condition = "", int branchId = 1)
        //{
        //    if (User.IsInRole("SUPER") || User.IsInRole("ADMIN"))
        //    {
        //        return (new MVCHelper()).BindDropdownList("DEPARTMENT", blank, deptId, condition, branchId);
        //    }
        //    else
        //    {
        //        return (new MVCHelper()).BindDropdownList("DEPARTMENTRIGHT", blank, deptId, GetStaffId().ToString(), branchId);
        //    }
        //}
    }
}