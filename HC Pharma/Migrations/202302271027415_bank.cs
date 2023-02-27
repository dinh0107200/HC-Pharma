namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "BankInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "BankInfo");
        }
    }
}
