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
    public class MenuHeaderService : IMenuHeaderService
    {
        private readonly UnitOfWork _unitOfWork;

        public MenuHeaderService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<MenuHeaderDTO> GetAllMenuHeader()
        {
            try
            {
                using (MenuHeaderRepository MenuHeaderRepo = new MenuHeaderRepository())
                {
                    var MenuHeaderDAOList = MenuHeaderRepo.MenuHeaderList();

                    var MenuHeaderDTOList = MenuHeaderMapper.GetAllMenuHeaderDTO(MenuHeaderDAOList).OrderBy(x=>x.StSortOrder).ToList();
                    return MenuHeaderDTOList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StockMenuView> GetStockMenuDetail(int mnId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@MnId", mnId);



                var ds = _unitOfWork.StockRepository.DataSetSqlQuery("[dbo].[sp_MenuStockDetail]", true,sqlParameters);

                List<StockMenuView> stockMenuViews = new();

                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow reader in ds.Tables[1].Rows)
                    {
                        stockMenuViews.Add(new StockMenuView()
                        {
                            StId = Convert.ToInt32(reader["StId"]),
                            StName = DBNullHandler.String(reader["StName"]),
                            StDes = DBNullHandler.String(reader["StDes"]),
                            StCode = DBNullHandler.String(reader["StCode"]),
                            StImage = DBNullHandler.String(reader["StImage"]),
                            StInActive = DBNullHandler.Int32(reader["StInActive"], 0),
                            StIsPopular = DBNullHandler.Int32(reader["StIsPopular"], 0),
                            StHSCode = DBNullHandler.String(reader["StHSCode"]),
                            StColour = DBNullHandler.String(reader["StColour"]),
                            StSize = DBNullHandler.String(reader["StSize"]),
                            StIsPant = DBNullHandler.Int(reader["StIsPant"]),
                            StIsShirt = DBNullHandler.Int(reader["StIsShirt"]),
                            StIsOther = DBNullHandler.Int(reader["StIsOther"]),
                            MenuHeaderId = Convert.ToInt32(reader["MenuHeaderId"]),
                            StMenuHeaderId = Convert.ToInt32(reader["StMenuHeaderId"]),
                            MenuHeaderName = DBNullHandler.String(reader["MenuHeaderName"]),
                            MenuHeaderIsActive = Convert.ToInt32(reader["MenuHeaderIsActive"]),

                        });
                    }
                }

                return stockMenuViews;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuHeaderDTO GetMenuHeaderByIdd(int MnId)
        {
            try
            {
                MenuHeaderRepository menuheaderrepo = new MenuHeaderRepository();
                var menuheader = menuheaderrepo.GetMenuHeaderByIdd(MnId);
                if (MnId > 0)
                {
                    return MenuHeaderMapper.GetMenuHeaderDTO(menuheader);
                }
                else
                {
                    return new MenuHeaderDTO();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuHeaderDTO GetMenuHeaderById(string MenuHeaderName)
        {
            try
            {
                using (MenuHeaderRepository MenuHeaderRepo = new MenuHeaderRepository())
                {
                    var MenuHeaderDAO = MenuHeaderRepo.GetMenuHeaderById(MenuHeaderName);
                    var MenuHeaderDTOMOdel = MenuHeaderMapper.GetMenuHeaderDTO(MenuHeaderDAO);

                    return MenuHeaderDTOMOdel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CreateMenuHeader(MenuHeaderDTO MenuHeaderUI)
        {
            try
            {
                using (MenuHeaderRepository MenuHeaderRepo = new MenuHeaderRepository())
                {
                    var MenuHeaderModel = MenuHeaderMapper.GetMenuHeaderDAO(MenuHeaderUI);
                    if (MenuHeaderModel.MenuHeaderId > 0)
                    {
                        var stockDAO = MenuHeaderRepo.GetMenuHeaderByIdd(MenuHeaderUI.MenuHeaderId);
                        MenuHeaderModel.MenuHeaderId = stockDAO.MenuHeaderId;

                        MenuHeaderModel = MenuHeaderModel.Adapt(stockDAO);
                        var result = MenuHeaderRepo.UpdateMenuHeader(MenuHeaderModel);
                        return MenuHeaderUI.MenuHeaderId;
                    }
                    else
                    {
                        return MenuHeaderRepo.CreateMenuHeader(MenuHeaderModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetValueByName(string MenuHeaderName)
        {
            try
            {
                using (MenuHeaderRepository MenuHeaderRepo = new MenuHeaderRepository())
                {
                    return MenuHeaderRepo.GetMenuHeaderById(MenuHeaderName).MenuHeaderName;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool DeleteMenuHeader(int mnId)
        {
            try
            {

                using (MenuHeaderRepository menuRepo = new MenuHeaderRepository())
                {

                    var result = menuRepo.DeleteMenuHeader(mnId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMenuHeader(MenuHeaderDTO MenuHeaderDTO)
        {
            try
            {
                using (MenuHeaderRepository MenuHeaderRepo = new MenuHeaderRepository())
                {
                    var MenuHeaderDAOModel = MenuHeaderRepo.GetMenuHeaderById(MenuHeaderDTO.MenuHeaderName);

                    var MenuHeaderModel = MenuHeaderMapper.GetMenuHeaderDAO(MenuHeaderDTO);

                    MenuHeaderModel.MenuHeaderName = MenuHeaderDAOModel.MenuHeaderName;

                    MenuHeaderModel = MenuHeaderModel.Adapt(MenuHeaderDAOModel);

                    MenuHeaderRepo.UpdateMenuHeader(MenuHeaderModel);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}