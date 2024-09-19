using System.Collections.Generic;
using System.Linq;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Mapper
{
    public class MenuHeaderMapper
    {
        public static List<MenuHeaderDTO> GetAllMenuHeaderDTO(List<MenuHeader> MenuHeaderList)
        {

            var MenuHeaderDTOList = MenuHeaderList.Select(x => new MenuHeaderDTO
            {
                MenuHeaderId = x.MenuHeaderId,
                MenuHeaderName = x.MenuHeaderName,
                MenuHeaderIsActive = x.MenuHeaderIsActive,
                StSortOrder = x.StSortOrder,
            }).ToList();

            return MenuHeaderDTOList;
        }

        public static MenuHeader GetMenuHeaderDAO(MenuHeaderDTO x)
        {

            return new MenuHeader()
            {
                MenuHeaderId = x.MenuHeaderId,
                MenuHeaderName = x.MenuHeaderName,
                MenuHeaderIsActive = x.MenuHeaderIsActive,
                StSortOrder = x.StSortOrder,
            };          
        }

        public static MenuHeaderDTO GetMenuHeaderDTO(MenuHeader x)
        {

            return new MenuHeaderDTO()
            {
                MenuHeaderId = x.MenuHeaderId,
                MenuHeaderName = x.MenuHeaderName,
                MenuHeaderIsActive = x.MenuHeaderIsActive,
                StSortOrder = x.StSortOrder,

            };
        }
    }
}
