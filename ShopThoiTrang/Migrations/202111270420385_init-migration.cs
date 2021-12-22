namespace ShopThoiTrang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        slug = c.String(),
                        parentid = c.Int(nullable: false),
                        orders = c.Int(nullable: false),
                        metakey = c.String(),
                        metadesc = c.String(),
                        created_at = c.DateTime(),
                        created_by = c.Int(),
                        updated_at = c.DateTime(),
                        updated_by = c.Int(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fullname = c.String(),
                        email = c.String(),
                        phone = c.String(),
                        title = c.String(maxLength: 255),
                        detail = c.String(storeType: "ntext"),
                        created_at = c.DateTime(storeType: "smalldatetime"),
                        created_by = c.Int(),
                        updated_at = c.DateTime(storeType: "smalldatetime"),
                        updated_by = c.Int(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.link",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        slug = c.String(),
                        tableId = c.Int(),
                        type = c.String(maxLength: 50),
                        parentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        type = c.String(),
                        link = c.String(),
                        tableid = c.Int(),
                        parentid = c.Int(nullable: false),
                        orders = c.Int(nullable: false),
                        position = c.String(),
                        created_at = c.DateTime(nullable: false),
                        created_by = c.Int(),
                        updated_at = c.DateTime(nullable: false),
                        updated_by = c.Int(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        userid = c.Int(nullable: false),
                        created_ate = c.DateTime(nullable: false),
                        exportdate = c.DateTime(),
                        deliveryaddress = c.String(),
                        deliveryname = c.String(),
                        deliveryphone = c.String(),
                        deliveryemail = c.String(),
                        deliveryPaymentMethod = c.String(),
                        StatusPayment = c.Int(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        updated_by = c.Int(),
                        status = c.Int(nullable: false),
                        totalSum = c.Double(),
                        transport_fee = c.Double(),
                        transport_type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ordersdetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        orderid = c.Int(nullable: false),
                        productid = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        quantity = c.Int(nullable: false),
                        Size = c.String(),
                        priceSale = c.Int(nullable: false),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.post",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        topid = c.Int(),
                        title = c.String(),
                        slug = c.String(),
                        detail = c.String(),
                        img = c.String(),
                        descriptionShort = c.String(),
                        type = c.String(),
                        metakey = c.String(),
                        metadesc = c.String(),
                        created_at = c.DateTime(nullable: false),
                        created_by = c.Int(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        updated_by = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        catid = c.Int(nullable: false),
                        name = c.String(),
                        slug = c.String(),
                        img = c.String(),
                        imgBehind = c.String(),
                        detail = c.String(),
                        price = c.Double(nullable: false),
                        pricesale = c.Double(nullable: false),
                        ProductType = c.String(),
                        number = c.Int(nullable: false),
                        sold = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        created_by = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        parentId = c.Int(nullable: false),
                        accessName = c.String(),
                        description = c.String(),
                        GropID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.size",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SizeS = c.Int(nullable: false),
                        SizeM = c.Int(nullable: false),
                        SizeL = c.Int(nullable: false),
                        SizeXL = c.Int(nullable: false),
                        SizeXXL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SizeShoe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Size_36 = c.Int(nullable: false),
                        Size_37 = c.Int(nullable: false),
                        Size_38 = c.Int(nullable: false),
                        Size_39 = c.Int(nullable: false),
                        Size_40 = c.Int(nullable: false),
                        Size_41 = c.Int(nullable: false),
                        Size_42 = c.Int(nullable: false),
                        Size_43 = c.Int(nullable: false),
                        Size_44 = c.Int(nullable: false),
                        Size_45 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.slider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        url = c.String(),
                        position = c.String(),
                        img = c.String(),
                        orders = c.Int(),
                        created_at = c.DateTime(nullable: false),
                        created_by = c.Int(),
                        updated_at = c.DateTime(nullable: false),
                        updated_by = c.Int(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.topic",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        slug = c.String(),
                        parentid = c.Int(nullable: false),
                        orders = c.Int(nullable: false),
                        metakey = c.String(),
                        metadesc = c.String(),
                        created_at = c.DateTime(nullable: false),
                        created_by = c.Int(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        updated_by = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fullname = c.String(),
                        username = c.String(),
                        password = c.String(),
                        email = c.String(),
                        gender = c.String(),
                        address = c.String(),
                        phone = c.String(),
                        img = c.String(),
                        access = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        created_by = c.Int(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        updated_by = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user");
            DropTable("dbo.topic");
            DropTable("dbo.slider");
            DropTable("dbo.SizeShoe");
            DropTable("dbo.size");
            DropTable("dbo.role");
            DropTable("dbo.Product");
            DropTable("dbo.post");
            DropTable("dbo.ordersdetail");
            DropTable("dbo.order");
            DropTable("dbo.menu");
            DropTable("dbo.link");
            DropTable("dbo.contact");
            DropTable("dbo.category");
        }
    }
}
