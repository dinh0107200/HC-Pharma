namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBannerClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "GroupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "GroupId");
        }
    }
}
