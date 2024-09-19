using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingPro.DataAccessLayer.Model
{
    [Table("Company")]
    public  class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? ContactNo { get; set; }
        public string? CompanyEmail { get; set; }
    }
}
