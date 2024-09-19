using ClothingPro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ClothingPro.BusinessLayer.DTO;
public class StockDTO
{
    public int StId { get; set; }

    [Required(ErrorMessage = "Stock Name is required.")]
    public string? StName { get; set; }
    public string? StDes { get; set; }
    public int? StInActive { get; set; }
    public int? StSortOrder { get; set; }
    public string? StCode { get; set; }
    public string? StImage { get; set; }
    public string? StImageUpdate { get; set; }
    public int? StIsPopular { get; set; }
    public string? StColour { get; set; }
    public string? StSize { get; set; }
    public string? StHSCode { get; set; }
    public int? StIsShirt { get; set; }
    public int? StIsPant { get; set; }
    public int? StIsOther { get; set; }
    public DateTime? StAddedDate { get; set; }
    public int? StMenuHeaderId { get; set; }
    public List<StockDTO>? stockPopularList { get; set; }
    public List<StockDTO>? stockPantsList { get; set; }
    public List<StockDTO>? stockLatestList { get; set; }
    public List<MenuHeaderDTO>? MenuHeaderLists { get; set; }
    public List<MenuHeaderDTO>? MenuHeaderList { get; set; }
    public List<StockMenuView>? StockMenuViewlists { get; set; }

}