namespace ShopThoiTrang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable_Permission_UserRoles_UserPermission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionAction = c.String(),
                        PermissionName = c.String(),
                        Status = c.Int(nullable: false),
                        CreateBy = c.Int(),
                        CreateAt = c.DateTime(),
                        LastModBy = c.Int(),
                        LastModAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Permission = c.String(),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreateAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRole");
            DropTable("dbo.UserPermission");
            DropTable("dbo.Permission");
        }
    }
}
