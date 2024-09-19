using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingPro.DataAccessLayer.Model
{
    [Table("MenuHeader")]
    public  class MenuHeader
    {
        [Key]
        public int MenuHeaderId { get; set; }
        public string MenuHeaderName { get; set; }
        public int? MenuHeaderIsActive { get; set; }
        public int? StSortOrder { get; set; }
    }
}
