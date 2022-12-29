namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConfigSites", "Zalo", c => c.String(maxLength: 500));
            AlterColumn("dbo.ConfigSites", "Tiki", c => c.String(maxLength: 500));
            AlterColumn("dbo.ConfigSites", "Tiktok", c => c.String(maxLength: 500));
            AlterColumn("dbo.ConfigSites", "Shopee", c => c.String(maxLength: 500));
            AlterColumn("dbo.ConfigSites", "Lazada", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConfigSites", "Lazada", c => c.String(maxLength: 50));
            AlterColumn("dbo.ConfigSites", "Shopee", c => c.String(maxLength: 50));
            AlterColumn("dbo.ConfigSites", "Tiktok", c => c.String(maxLength: 50));
            AlterColumn("dbo.ConfigSites", "Tiki", c => c.String(maxLength: 50));
            AlterColumn("dbo.ConfigSites", "Zalo", c => c.String(maxLength: 50));
        }
    }
}
