using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingPro.DataAccessLayer.Model
{
    [Table("Banner")]
    public  class Banner
    {
        [Key]
        public int BannerId { get; set; }
        public string BannerImg { get; set; }
        public int? BannerIsActive { get; set; }
        public int? BannerSortOrder { get; set; }
    }
}
