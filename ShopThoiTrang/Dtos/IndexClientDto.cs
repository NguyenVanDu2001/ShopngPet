using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Dtos
{
    public class IndexClientDto
    {
        public List<Mcategory> listCate { get; set; }
        public List<Product> ListProducts { get; set; }
        public List<Mpost> ListPosts { get; set; }
    }
}