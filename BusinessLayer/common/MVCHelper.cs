using ClothingPro.BusinessLayer.Repository;
using ClothingPro.DataAccessLayer.DbAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace ClothingPro.BusinessLayer.Helper;

public class MVCHelper
{
    private readonly UnitOfWork _unitOfWork;
    public MVCHelper()
    {
        _unitOfWork = new UnitOfWork();
    }
    public SelectList BindDropdownList(string searchFor, bool blank = false, string defaultValue = "", string condition = "", int? branchId = 1)
    {
        List<SelectListItem> returnListItems = new List<SelectListItem>();

        switch (searchFor)
        {

            #region "STOCK"
            case "STOCK":
                var stockItems = _unitOfWork.StockRepository.GetAll().OrderBy(x => x.StDes).ToList();

                foreach (var item in stockItems)
                {
                    returnListItems.Add(new SelectListItem
                    {
                        Text = item.StDes,
                        Value = item.StId.ToString(),
                        Selected = item.StId.ToString() == defaultValue
                    });
                }
                break;
            #endregion

            #region STOCKISPOPULAR
            case "STOCKISPOPULAR":
                var stockPopularItems = _unitOfWork.StockRepository.GetAll().OrderBy(x => x.StIsPopular == 1).ToList();

                foreach (var item in stockPopularItems)
                {
                    returnListItems.Add(new SelectListItem
                    {
                        Text = item.StName,
                        Value = item.StId.ToString(),
                        Selected = item.StId.ToString() == defaultValue
                    });
                }
                break;

            #endregion

            #region STOCKISPANTS
            case "STOCKISPANTS":
                var stockPantsItems = _unitOfWork.StockRepository.GetAll().OrderBy(x => x.StIsPant == 1).ToList();

                foreach (var item in stockPantsItems)
                {
                    returnListItems.Add(new SelectListItem
                    {
                        Text = item.StName,
                        Value = item.StId.ToString(),
                        Selected = item.StId.ToString() == defaultValue
                    });
                }
                break;

            #endregion
            #region MENUHEADER
            case "MENUHEADER":
                var menuheader = _unitOfWork.MenuHeaderRepository.GetAll().OrderBy(x => x.MenuHeaderIsActive != 1).ToList();

                foreach (var item in menuheader)
                {
                    returnListItems.Add(new SelectListItem
                    {
                        Text = item.MenuHeaderName,
                        Value = item.MenuHeaderId.ToString(),
                        Selected = item.MenuHeaderId.ToString() == defaultValue
                    });
                }
                break;

                #endregion
        }

        if (blank)
        {
            returnListItems.Insert(0, new SelectListItem() { Value = "", Text = "", Selected = true });
        }

        return new SelectList(returnListItems, "Value", "Text", defaultValue);
    }

    //public string GetStaffName(int Id)
    //{
    //    var staffmodel = _unitOfWork.StaffRepository.FindById(Id);
    //    if (staffmodel != null)
    //    {
    //        return staffmodel.StfName;
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}



    public bool CHECK_DUPLICATE(string tblName, string Condtion, string ConditionValue, string CheckFor, string CheckForValue)
    {
        try
        {
            SqlParameter[] sqlparam = new SqlParameter[5];

            sqlparam[0] = new SqlParameter("@TBLNAME", SqlDbType.NVarChar);
            sqlparam[0].Value = NullHandler.String(tblName);
            sqlparam[1] = new SqlParameter("@CONDITION", SqlDbType.VarChar, 50);
            sqlparam[1].Value = NullHandler.String(Condtion);
            sqlparam[2] = new SqlParameter("@VALUE", SqlDbType.VarChar, 50);
            sqlparam[2].Value = NullHandler.String(ConditionValue);
            sqlparam[3] = new SqlParameter("@CHECKFOR", SqlDbType.VarChar, 50);
            sqlparam[3].Value = NullHandler.String(CheckFor);
            sqlparam[4] = new SqlParameter("@CHECKFORVALUE", SqlDbType.VarChar, 100);
            sqlparam[4].Value = NullHandler.String(CheckForValue);

            var ds = _unitOfWork.SettingRepository.DataSetSqlQuery("[dbo].[sp_CHECK_DUPLICATE]", true, sqlparam);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return true;
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
}