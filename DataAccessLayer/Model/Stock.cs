using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingPro.DataAccessLayer.Model;

[Table("Stock")]
public class Stock
{
    [Key]
    public int StId { get; set; }

    [Required(ErrorMessage = "Stock Name is required.")]
    public string StName { get; set; }
    public string StDes { get; set; }
    public int? StInActive { get; set; }
    public int? StSortOrder { get; set; }
    public string? StCode { get; set; }
    public string? StImage { get; set; }
    public int? StIsPopular { get; set; }
    public string? StColour { get; set; }
    public string? StSize { get; set; }
    public string? StHSCode { get; set; }
    public int? StIsShirt { get; set; }
    public int? StIsPant { get; set; }
    public int? StIsOther { get; set; }
    public int? StMenuHeaderId { get; set; }   
    public DateTime? StAddedDate { get; set; }
}
