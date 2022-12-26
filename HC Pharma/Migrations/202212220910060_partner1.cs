namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partner1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partners", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partners", "Sort");
        }
    }
}
