namespace ShopThoiTrang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_entity_Foreign_all_table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ordersdetail", "orderid", c => c.Int());
            CreateIndex("dbo.ordersdetail", "orderid");
            CreateIndex("dbo.Product", "catid");
            CreateIndex("dbo.ordersdetail", "productid");
            CreateIndex("dbo.order", "userid");
            CreateIndex("dbo.post", "topid");
            CreateIndex("dbo.size", "ProductId");
            AddForeignKey("dbo.ordersdetail", "orderid", "dbo.order", "ID");
            AddForeignKey("dbo.Product", "catid", "dbo.category", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ordersdetail", "productid", "dbo.Product", "ID", cascadeDelete: true);
            AddForeignKey("dbo.order", "userid", "dbo.user", "ID", cascadeDelete: true);
            AddForeignKey("dbo.post", "topid", "dbo.topic", "ID");
            AddForeignKey("dbo.size", "ProductId", "dbo.Product", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.size", "ProductId", "dbo.Product");
            DropForeignKey("dbo.post", "topid", "dbo.topic");
            DropForeignKey("dbo.order", "userid", "dbo.user");
            DropForeignKey("dbo.ordersdetail", "productid", "dbo.Product");
            DropForeignKey("dbo.Product", "catid", "dbo.category");
            DropForeignKey("dbo.ordersdetail", "orderid", "dbo.order");
            DropIndex("dbo.size", new[] { "ProductId" });
            DropIndex("dbo.post", new[] { "topid" });
            DropIndex("dbo.order", new[] { "userid" });
            DropIndex("dbo.ordersdetail", new[] { "productid" });
            DropIndex("dbo.Product", new[] { "catid" });
            DropIndex("dbo.ordersdetail", new[] { "orderid" });
            AlterColumn("dbo.ordersdetail", "orderid", c => c.Int(nullable: false));
        }
    }
}
