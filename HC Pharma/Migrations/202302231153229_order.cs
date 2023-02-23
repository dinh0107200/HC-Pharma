namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddColumn("dbo.OrderDetails", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(precision: 18, scale: 2));
            AddPrimaryKey("dbo.OrderDetails", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AlterColumn("dbo.OrderDetails", "Price", c => c.Int());
            DropColumn("dbo.OrderDetails", "Id");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderId", "ProductId" });
        }
    }
}
