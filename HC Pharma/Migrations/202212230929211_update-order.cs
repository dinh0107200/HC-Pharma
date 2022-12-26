namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductName", c => c.String());
            AddColumn("dbo.Orders", "ProductPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ProductUrl", c => c.String());
            AddColumn("dbo.Orders", "ProductImg", c => c.String());
            AddColumn("dbo.Orders", "TotalPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Quantity", c => c.Int());
            AddColumn("dbo.Products", "Order_Id", c => c.Int());
            AlterColumn("dbo.Orders", "CustomerInfo_Mobile", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Products", "Order_Id");
            AddForeignKey("dbo.Products", "Order_Id", "dbo.Orders", "Id");
            DropColumn("dbo.Orders", "MaDonHang");
            DropColumn("dbo.Orders", "Viewed");
            DropTable("dbo.OrderDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Viewed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "MaDonHang", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_Id" });
            AlterColumn("dbo.Orders", "CustomerInfo_Mobile", c => c.String(nullable: false, maxLength: 11));
            DropColumn("dbo.Products", "Order_Id");
            DropColumn("dbo.Orders", "Quantity");
            DropColumn("dbo.Orders", "TotalPrice");
            DropColumn("dbo.Orders", "ProductImg");
            DropColumn("dbo.Orders", "ProductUrl");
            DropColumn("dbo.Orders", "ProductPrice");
            DropColumn("dbo.Orders", "ProductName");
            DropColumn("dbo.Orders", "ProductId");
            CreateIndex("dbo.OrderDetails", "ProductId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
