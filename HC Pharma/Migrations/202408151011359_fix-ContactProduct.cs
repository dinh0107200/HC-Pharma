namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixContactProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactProducts", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactProducts", "Email", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
