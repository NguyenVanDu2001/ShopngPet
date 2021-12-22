namespace ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contact")]
    public partial class Mcontact
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        [StringLength(255)]
        public string title { get; set; }
        [Column(TypeName = "ntext")]
        public string detail { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public int status { get; set; }
    }
}
