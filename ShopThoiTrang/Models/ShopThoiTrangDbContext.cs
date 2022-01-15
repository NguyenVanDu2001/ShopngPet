using System.Data.Entity;

namespace ShopThoiTrang.Models
{
    public class ShopThoiTrangDbContext : DbContext
    {
        public ShopThoiTrangDbContext() : base("name=ChuoiKN")
        { }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Mcategory> Categorys { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Morder> Orders { get; set; }
        public virtual DbSet<Mordersdetail> Ordersdetails { get; set; }
        public virtual DbSet<role> Roles { get; set; }
        public virtual DbSet<Mmenu> Menus { get; set; }
        public virtual DbSet<Mpost> Posts { get; set; }
        public virtual DbSet<Mtopic> Topics { get; set; }
        public virtual DbSet<Muser> Users { get; set; }
        public virtual DbSet<Mslider> Sliders { get; set; }
        public virtual DbSet<Mcontact> Contacts { get; set; }
        public virtual DbSet<link> Link { get; set; }
        public virtual DbSet<SizeShoe> SizeShoes { get; set; }
        public virtual DbSet<Attribute>Attributes { get; set; }

    }
}
