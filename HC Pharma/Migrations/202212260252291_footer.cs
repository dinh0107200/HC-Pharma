namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class footer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleCategories", "ShowFooter", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProductCategories", "ShowFooter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "ShowFooter", c => c.Boolean(nullable: false));
            DropColumn("dbo.ArticleCategories", "ShowFooter");
        }
    }
}
