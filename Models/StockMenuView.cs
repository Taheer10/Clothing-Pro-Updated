using ClothingPro.BusinessLayer.DTO;

namespace ClothingPro.Models
{
    public class StockMenuView
    {
        public int MenuHeaderId { get; set; }
        public string MenuHeaderName { get; set; }
        public int? MenuHeaderIsActive { get; set; }
        public int StId { get; set; }
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
        public string? StSearchValue { get; set; }
        public int? StIsShirt { get; set; }
        public int? StIsPant { get; set; }
        public int? StIsOther { get; set; }
        public DateTime? StAddedDate { get; set; }
        public int? StMenuHeaderId { get; set; }
        public List<StockMenuView>? stockmenusearchlist {  get; set; } 
        public List<StockMenuView>? StockMenuViewlists {  get; set; } 
        public List<MenuHeaderDTO>? MenuHeaderList {  get; set; }
        
    }
}
