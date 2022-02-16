using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Models
{
    [Table("UserPermission")]
    public class UserPermission
    {
        [Key]
        public int Id { get; set; }
        public string Permission { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}