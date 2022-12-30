namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Introducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slogan = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Introducts");
        }
    }
}
