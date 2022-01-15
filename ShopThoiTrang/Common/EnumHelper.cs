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
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DisplayAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
    public enum AttributeHelper
    {
        [Display(Description = "Kích thước")]
        KICHTHUOC = 1,
        [Display(Description = "Kích thước")]
        //[Description("Màu sắc")]
        MAUSAC = 2,


    }
}