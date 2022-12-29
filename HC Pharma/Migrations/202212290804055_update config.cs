namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateconfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "Mess", c => c.String(maxLength: 50));
            AddColumn("dbo.ConfigSites", "Tiki", c => c.String(maxLength: 50));
            AddColumn("dbo.ConfigSites", "Tiktok", c => c.String(maxLength: 50));
            AddColumn("dbo.ConfigSites", "Shopee", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "Shopee");
            DropColumn("dbo.ConfigSites", "Tiktok");
            DropColumn("dbo.ConfigSites", "Tiki");
            DropColumn("dbo.ConfigSites", "Mess");
        }
    }
}
