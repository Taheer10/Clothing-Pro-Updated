namespace ClothingPro.BusinessLayer.DTO
{
    public class ColorImagesDTO
    {
        public int ColorImagesId { get; set; }
        public string ColorImagesName { get; set; }
        public string ColorImagesImg { get; set; }
        public string? ColorName { get; set; }
        public int StockId { get; set; }
        public int? colorImagesListId { get; set; }
        public List<ColorImagesDTO>? colorImagesList {  get; set; }

    }
}
