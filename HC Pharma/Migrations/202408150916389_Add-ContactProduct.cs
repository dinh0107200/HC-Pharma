namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IpAddress = c.String(),
                        Fullname = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 100),
                        Body = c.String(maxLength: 4000),
                        ContactNeeds = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ContactProducts", new[] { "ProductId" });
            DropTable("dbo.ContactProducts");
        }
    }
}
