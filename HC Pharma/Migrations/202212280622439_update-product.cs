namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Producer", c => c.String());
            AddColumn("dbo.Products", "Origin", c => c.String());
            AddColumn("dbo.Products", "Specifications", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Specifications");
            DropColumn("dbo.Products", "Origin");
            DropColumn("dbo.Products", "Producer");
        }
    }
}
