namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConfigSites", "BankInfo");
            DropColumn("dbo.ConfigSites", "Hotline2");
            DropColumn("dbo.ConfigSites", "Hotline3");
            DropColumn("dbo.ProductCategories", "Hot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "Hot", c => c.Boolean(nullable: false));
            AddColumn("dbo.ConfigSites", "Hotline3", c => c.String(maxLength: 50));
            AddColumn("dbo.ConfigSites", "Hotline2", c => c.String(maxLength: 50));
            AddColumn("dbo.ConfigSites", "BankInfo", c => c.String());
        }
    }
}
