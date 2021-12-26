namespace ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class Morder
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }
        public string code { get; set; }
        [ForeignKey("userid")]
        public virtual Muser Muser { get; set; }
        public int userid { get; set; }
        public DateTime created_ate { get; set; }
        public DateTime? exportdate { get; set; }
        public string deliveryaddress { get; set; }
        public string deliveryname { get; set; }
        public string deliveryphone { get; set; }
        public string deliveryemail { get; set; }
        public string deliveryPaymentMethod { get; set; }
        public int StatusPayment { get; set; }
        public DateTime updated_at { get; set; }
        public int? updated_by { get; set; }
        public int status { get; set; }
        public double? totalSum { get; set; }
        public double? transport_fee { get; set; }
        public string transport_type { get; set; }
        public virtual ICollection<Mordersdetail> Mordersdetails { get; set; }
    }
}
