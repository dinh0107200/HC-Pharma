namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class config : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "Lazada", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "Lazada");
        }
    }
}
