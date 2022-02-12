namespace ShopThoiTrang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimgcate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.category", "img", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.category", "img");
        }
    }
}
