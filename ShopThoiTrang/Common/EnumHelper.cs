using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Common
{
    public class EnumHelper
    {
        
    }
    public enum AttributeHelper
    {
        [Display(Description = "Kích thước")]
        KICHTHUOC = 1,
        [Display(Description = "Màu sắc")]
        //[Description("Màu sắc")]
        MAUSAC = 2,


    }
}