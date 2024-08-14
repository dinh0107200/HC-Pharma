namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upLandingPage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannerLandingPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(),
                        Content = c.String(maxLength: 700),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PostionAbout = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Comboes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProductId = c.Int(),
                        Sort = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        Image = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.FeedbackProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(),
                        UrlVideo = c.String(),
                        FileVideo = c.String(),
                        Content = c.String(maxLength: 700),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.LandingPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageIntro = c.String(),
                        Intro = c.String(),
                        Imagesolution = c.String(),
                        Solution = c.String(),
                        ImageAnalysis = c.String(),
                        ProductAnalysis = c.String(),
                        CommonDiseases = c.String(),
                        CauseDisease = c.String(),
                        QA = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.QaProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        Sort = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Carts", "ComboId", c => c.Int());
            AddColumn("dbo.OrderDetails", "NameCombo", c => c.String());
            CreateIndex("dbo.Carts", "ComboId");
            AddForeignKey("dbo.Carts", "ComboId", "dbo.Comboes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QaProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.LandingPages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.FeedbackProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "ComboId", "dbo.Comboes");
            DropForeignKey("dbo.Comboes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BannerLandingPages", "ProductId", "dbo.Products");
            DropIndex("dbo.QaProducts", new[] { "ProductId" });
            DropIndex("dbo.LandingPages", new[] { "ProductId" });
            DropIndex("dbo.FeedbackProducts", new[] { "ProductId" });
            DropIndex("dbo.Comboes", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "ComboId" });
            DropIndex("dbo.BannerLandingPages", new[] { "ProductId" });
            DropColumn("dbo.OrderDetails", "NameCombo");
            DropColumn("dbo.Carts", "ComboId");
            DropTable("dbo.QaProducts");
            DropTable("dbo.LandingPages");
            DropTable("dbo.FeedbackProducts");
            DropTable("dbo.Comboes");
            DropTable("dbo.BannerLandingPages");
        }
    }
}
