namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_Id" });
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Prefix = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Prefix = c.String(maxLength: 20),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Prefix = c.String(maxLength: 20),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Orders", "MaDonHang", c => c.String(maxLength: 50));
            AddColumn("dbo.Orders", "Payment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "TypePay", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Transport", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "TransportDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Viewed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "CustomerInfo_IsNewMember", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "ThanhToanTruoc", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ShipFee", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CityId", c => c.Int());
            AddColumn("dbo.Orders", "DistrictId", c => c.Int());
            AddColumn("dbo.Orders", "WardId", c => c.Int());
            AlterColumn("dbo.Orders", "CustomerInfo_Mobile", c => c.String(nullable: false, maxLength: 11));
            CreateIndex("dbo.Orders", "CityId");
            CreateIndex("dbo.Orders", "DistrictId");
            CreateIndex("dbo.Orders", "WardId");
            AddForeignKey("dbo.Orders", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Orders", "DistrictId", "dbo.Districts", "Id");
            AddForeignKey("dbo.Orders", "WardId", "dbo.Wards", "Id");
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "ProductName");
            DropColumn("dbo.Orders", "ProductPrice");
            DropColumn("dbo.Orders", "ProductUrl");
            DropColumn("dbo.Orders", "ProductImg");
            DropColumn("dbo.Orders", "TotalPrice");
            DropColumn("dbo.Orders", "Quantity");
            DropColumn("dbo.Products", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Order_Id", c => c.Int());
            AddColumn("dbo.Orders", "Quantity", c => c.Int());
            AddColumn("dbo.Orders", "TotalPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ProductImg", c => c.String());
            AddColumn("dbo.Orders", "ProductUrl", c => c.String());
            AddColumn("dbo.Orders", "ProductPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ProductName", c => c.String());
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "WardId", "dbo.Wards");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Orders", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Wards", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Districts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "WardId" });
            DropIndex("dbo.Orders", new[] { "DistrictId" });
            DropIndex("dbo.Orders", new[] { "CityId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Wards", new[] { "DistrictId" });
            DropIndex("dbo.Districts", new[] { "CityId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            AlterColumn("dbo.Orders", "CustomerInfo_Mobile", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Orders", "WardId");
            DropColumn("dbo.Orders", "DistrictId");
            DropColumn("dbo.Orders", "CityId");
            DropColumn("dbo.Orders", "ShipFee");
            DropColumn("dbo.Orders", "ThanhToanTruoc");
            DropColumn("dbo.Orders", "CustomerInfo_IsNewMember");
            DropColumn("dbo.Orders", "Viewed");
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "TransportDate");
            DropColumn("dbo.Orders", "Transport");
            DropColumn("dbo.Orders", "TypePay");
            DropColumn("dbo.Orders", "Payment");
            DropColumn("dbo.Orders", "MaDonHang");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Wards");
            DropTable("dbo.Districts");
            DropTable("dbo.Cities");
            DropTable("dbo.Carts");
            CreateIndex("dbo.Products", "Order_Id");
            AddForeignKey("dbo.Products", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
