namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixLandingPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LandingPage", c => c.Boolean(nullable: false));
            AddColumn("dbo.LandingPages", "Title", c => c.String());
            AddColumn("dbo.LandingPages", "SubTitle", c => c.String());
            AddColumn("dbo.LandingPages", "SubTitle2", c => c.String());
            AddColumn("dbo.LandingPages", "Imageregistration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandingPages", "Imageregistration");
            DropColumn("dbo.LandingPages", "SubTitle2");
            DropColumn("dbo.LandingPages", "SubTitle");
            DropColumn("dbo.LandingPages", "Title");
            DropColumn("dbo.Products", "LandingPage");
        }
    }
}
