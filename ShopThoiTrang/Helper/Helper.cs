using ShopThoiTrang.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Helper
{
    public static class Helper
    {
        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj)
         where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return (Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(
                enu => new SelectListItem() { Text = enu.ToString(), Value = enu.ToString()})).ToList();
        }
        public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new SelectListItem() { Text = GetDescriptionFromEnumValue((Enum)(object)enu), Value = (Convert.ToInt32((Enum)(object)enu)).ToString() })).ToList();
        }

        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DisplayAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() as DisplayAttribute;
            return attribute == null ? "" : attribute.Description;
        }
        public static int EnumToInt<TValue>(this TValue value) where TValue : Enum => Convert.ToInt32(value);
        //public static string GetDescriptionFromEnumInt<T>(int intValue)
        //{
        //    T stringEnum = (T)Enum.ToObject(typeof(Status), intValue);
        //    return GetDescriptionFromEnumValue();
        //}
    }
}