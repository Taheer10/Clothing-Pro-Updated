using System.Collections.Generic;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.Models;

namespace ClothingPro.BusinessLayer.Interface
{
    public interface IMenuHeaderService
    {
        List<MenuHeaderDTO> GetAllMenuHeader();
        string GetValueByName(string MenuHeaderName);
        void UpdateMenuHeader(MenuHeaderDTO MenuHeaderUi);
        MenuHeaderDTO GetMenuHeaderById(string MenuHeaderName);
        bool DeleteMenuHeader(int mnId);
        MenuHeaderDTO GetMenuHeaderByIdd(int MnId);
        int CreateMenuHeader(MenuHeaderDTO MenuHeaderUI);
        List<StockMenuView> GetStockMenuDetail(int mnId);
    }
}