namespace ShopThoiTrang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editentityattr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attribute", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attribute", "Value");
        }
    }
}
