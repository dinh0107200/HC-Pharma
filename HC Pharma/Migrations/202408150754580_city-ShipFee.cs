namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityShipFee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "ShipFee", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cities", "ShipFee");
        }
    }
}
