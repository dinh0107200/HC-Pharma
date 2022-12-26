namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Feedbacks", "Place");
            DropColumn("dbo.Feedbacks", "Star");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "Star", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "Place", c => c.String(maxLength: 100));
        }
    }
}
