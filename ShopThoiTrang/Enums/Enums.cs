using System.ComponentModel.DataAnnotations;

namespace ShopThoiTrang.Enums
{
    public enum Status
    {
        [Display(Description = "Hoạt động")]
        Active = 1,

        [Display(Description = "Tắt")]
        UnActive = 0
    }
}