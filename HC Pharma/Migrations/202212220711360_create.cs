namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 60),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Url = c.String(maxLength: 500),
                        CategorySort = c.Int(nullable: false),
                        CategoryActive = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        ShowMenu = c.Boolean(nullable: false),
                        TitleMeta = c.String(maxLength: 100),
                        DescriptionMeta = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        TypePost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false, maxLength: 500),
                        Body = c.String(),
                        Image = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        View = c.Int(nullable: false),
                        ArticleCategoryId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Home = c.Boolean(nullable: false),
                        Menu = c.Boolean(nullable: false),
                        Url = c.String(maxLength: 300),
                        TitleMeta = c.String(maxLength: 100),
                        DescriptionMeta = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategoryId, cascadeDelete: true)
                .Index(t => t.ArticleCategoryId);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerName = c.String(nullable: false, maxLength: 100),
                        Slogan = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        Url = c.String(maxLength: 500),
                        Sort = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConfigSites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Facebook = c.String(maxLength: 500),
                        Youtube = c.String(maxLength: 500),
                        Twitter = c.String(maxLength: 500),
                        Instagram = c.String(maxLength: 500),
                        Skype = c.String(maxLength: 500),
                        GooglePlus = c.String(maxLength: 500),
                        Pinterest = c.String(maxLength: 500),
                        LiveChat = c.String(maxLength: 4000),
                        Image = c.String(),
                        Favicon = c.String(),
                        GoogleMap = c.String(maxLength: 4000),
                        GoogleAnalytics = c.String(maxLength: 4000),
                        Place = c.String(),
                        Title = c.String(maxLength: 200),
                        AboutImage = c.String(),
                        AboutText = c.String(),
                        AboutUrl = c.String(maxLength: 500),
                        Description = c.String(maxLength: 500),
                        Hotline = c.String(maxLength: 50),
                        Zalo = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        InfoContact = c.String(),
                        InfoFooter = c.String(),
                        BankInfo = c.String(),
                        Hotline2 = c.String(maxLength: 50),
                        Hotline3 = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 100),
                        Body = c.String(maxLength: 4000),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 700),
                        Image = c.String(maxLength: 500),
                        Name = c.String(nullable: false, maxLength: 100),
                        Place = c.String(maxLength: 100),
                        Star = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaDonHang = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                        CustomerInfo_Fullname = c.String(nullable: false, maxLength: 50),
                        CustomerInfo_Address = c.String(nullable: false, maxLength: 200),
                        CustomerInfo_Mobile = c.String(nullable: false, maxLength: 11),
                        CustomerInfo_Email = c.String(nullable: false, maxLength: 50),
                        CustomerInfo_Body = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        Uses = c.String(),
                        Body = c.String(),
                        Usermanual = c.String(),
                        ListImage = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        Star = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Home = c.Boolean(nullable: false),
                        Url = c.String(maxLength: 300),
                        TitleMeta = c.String(maxLength: 100),
                        DescriptionMeta = c.String(maxLength: 500),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 80),
                        Url = c.String(maxLength: 500),
                        CategorySort = c.Int(nullable: false),
                        TitleIntroduce = c.String(maxLength: 500),
                        Description = c.String(),
                        CategoryActive = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        ShowMenu = c.Boolean(nullable: false),
                        Hot = c.Boolean(nullable: false),
                        ShowHome = c.Boolean(nullable: false),
                        ShowFooter = c.Boolean(nullable: false),
                        TitleMeta = c.String(maxLength: 100),
                        DescriptionMeta = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        CoverImage = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                        Coment = c.String(maxLength: 500),
                        Rate = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Articles", "ArticleCategoryId", "dbo.ArticleCategories");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Articles", new[] { "ArticleCategoryId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Contacts");
            DropTable("dbo.ConfigSites");
            DropTable("dbo.Banners");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleCategories");
            DropTable("dbo.Admins");
        }
    }
}
