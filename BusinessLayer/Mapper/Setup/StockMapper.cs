using System.Collections.Generic;
using System.Linq;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Mapper;
public class StockMapper
{
    public static List<StockDTO> GetStockDTOList(List<Stock> stockList)
    {
        var stockDTOList = stockList.Select(x => new StockDTO
        {
            StId = x.StId,
            StName = x.StName, 
            StDes = x.StDes,
            StInActive = x.StInActive,
            StCode = x.StCode,
            StIsOther = x.StIsOther,
            StIsShirt = x.StIsShirt,
            StIsPant = x.StIsPant,
            StImage  = x.StImage,
            StIsPopular= x.StIsPopular,
            StColour= x.StColour,
            StSize= x.StSize,
            StHSCode = x.StHSCode,
            StSortOrder = x.StSortOrder,
            StAddedDate = x.StAddedDate,
            StMenuHeaderId = x.StMenuHeaderId,


        }).ToList();
        return stockDTOList;
    }

    public static List<Stock> GetStockList(List<StockDTO> dtos)
    {
        var stockList = dtos.Select(x => new Stock
        {
            StId = x.StId,
            StName = x.StName,
            StDes = x.StDes,
            StInActive = x.StInActive,
            StCode = x.StCode,
            StIsOther = x.StIsOther,
            StIsShirt = x.StIsShirt,
            StIsPant = x.StIsPant,
            StImage = x.StImage,
            StIsPopular = x.StIsPopular,
            StColour = x.StColour,
            StSize = x.StSize,
            StHSCode = x.StHSCode,
            StSortOrder = x.StSortOrder,
            StAddedDate = x.StAddedDate,
            StMenuHeaderId = x.StMenuHeaderId,


        }).ToList();

        return stockList;
    }

    public static StockDTO GetStockDTO(Stock x)
    {
        return new StockDTO()
        {
            StId = x.StId,
            StName = x.StName,
            StDes = x.StDes,
            StInActive = x.StInActive,
            StCode = x.StCode,
            StIsOther = x.StIsOther,
            StIsShirt = x.StIsShirt,
            StIsPant = x.StIsPant,
            StImage = x.StImage,
            StIsPopular = x.StIsPopular,
            StColour = x.StColour,
            StSize = x.StSize,
            StHSCode = x.StHSCode,
            StSortOrder = x.StSortOrder,
            StAddedDate = x.StAddedDate,
            StMenuHeaderId = x.StMenuHeaderId,


        };
    }

    public static Stock GetStockDAO(StockDTO x)
    {
        return new Stock()
        {
            StId = x.StId,
            StName = x.StName,
            StDes = x.StDes,
            StInActive = x.StInActive,
            StCode = x.StCode,
            StIsOther = x.StIsOther,
            StIsShirt = x.StIsShirt,
            StIsPant = x.StIsPant,
            StImage = x.StImage,
            StIsPopular = x.StIsPopular,
            StColour = x.StColour,
            StSize = x.StSize,
            StHSCode = x.StHSCode,
            StSortOrder = x.StSortOrder,
            StAddedDate = x.StAddedDate,
            StMenuHeaderId = x.StMenuHeaderId,

        };
    }
}
