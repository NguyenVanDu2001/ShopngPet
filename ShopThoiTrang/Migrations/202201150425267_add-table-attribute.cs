namespace ShopThoiTrang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableattribute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        created_at = c.DateTime(),
                        created_by = c.Int(),
                        updated_at = c.DateTime(),
                        updated_by = c.Int(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.category", "JsonListAttrId", c => c.String());
            AddColumn("dbo.Product", "JsonListAttr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "JsonListAttr");
            DropColumn("dbo.category", "JsonListAttrId");
            DropTable("dbo.Attribute");
        }
    }
}
