using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HC_Pharma.Models;
using Microsoft.Ajax.Utilities;

namespace HC_Pharma.DAL
{
    public class DataEntities : DbContext
    {
        public DataEntities() : base("name=DataEntities") { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ConfigSite> ConfigSites { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Introduct> Introducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<FeedbackProduct> FeedbackProducts { get; set; }

        public DbSet<Combo> Combos { get; set; }
        public DbSet<LandingPage> LandingPages { get; set; }
        public DbSet<QaProduct> QaProducts { get; set; }
        public DbSet<BannerLandingPage> BannerLandingPages { get; set; }
        public DbSet<ContactProduct> ContactProducts { get; set; }

    }
}