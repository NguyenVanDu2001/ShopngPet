namespace ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SizeShoe")]
    public partial class SizeShoe
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Size_36 { get; set; }
        public int Size_37 { get; set; }
        public int Size_38 { get; set; }
        public int Size_39 { get; set; }
        public int Size_40 { get; set; }
        public int Size_41 { get; set; }
        public int Size_42 { get; set; }
        public int Size_43 { get; set; }
        public int Size_44 { get; set; }
        public int Size_45 { get; set; }
    }
}
