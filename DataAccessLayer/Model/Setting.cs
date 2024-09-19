using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingPro.DataAccessLayer.Model
{
    [Table("Setting")]
    public  class Setting
    {
        [Key]
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
        public bool SettingType { get; set; }
    }
}
