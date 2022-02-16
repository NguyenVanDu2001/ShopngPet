using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Models
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string PermissionAction { get; set; }
        public string PermissionName { get; set; }
        public int Status { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? LastModBy { get; set; }
        public DateTime? LastModAt { get; set; }
    }
}