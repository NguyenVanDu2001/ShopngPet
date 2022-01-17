namespace ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductModel
    {
      
        public int ID { get; set; }
        public int catid { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string img { get; set; }
        public string imgBehind { get; set; }
        public string detail { get; set; }
        public double price { get; set; }
        public double pricesale { get; set; }
        public string ProductType { get; set; }
        public int number { get; set; }
        public int sold { get; set; }
        public DateTime created_at { get; set; }
        public int created_by { get; set; }  
        public int status { get; set; }
        // phan biet san pham giay, quan ao, phu kien
        public int type { get; set; }
        public string  JsonListAttr { get; set; }
        public List<int>  JsonListAttrInt { get; set; }

    }
}
