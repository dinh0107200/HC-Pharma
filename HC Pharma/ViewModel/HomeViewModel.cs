using HC_Pharma.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace HC_Pharma.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<ProductCategory> Categories { get; set; }
        public IEnumerable<Product> Products { get; set;}
        public IEnumerable<Article> Articles { get; set;}
        public IEnumerable<Banner> Banner { get; set;}
        public IEnumerable<Feedback> Feedbacks { get; set; }
        public IEnumerable<Introduct> Introducts { get; set; }
        public  IEnumerable<Partner> Partners { get; set; }
    }
    public class HeaderViewModel
    {
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<ProductCategory> FilterCategories { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IEnumerable<ArticleCategory> IntroduceCat { get; set; }
    }
    public class FooterViewModel
    {
        public IEnumerable<ArticleCategory> ArticleCat { get; set; }
        public IEnumerable<ArticleCategory> Policy { get; set; }
        public IEnumerable<ArticleCategory> Introduct { get; set; }

    }
    public class CategoryProductViewModel
    {
        public int? CatId { get; set; }
        public ProductCategory Category { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public IPagedList<Product> Products { get; set; }
        public string Url { get; set; }
        public string Sort { set; get; }
        public int ProductResult { get; set; }
        public int BeginCount { get; set; }
        public int EndCount { get; set; }
    }
    public class ProductDetailViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public Review Review { get; set; }
        public ProductCategory Category { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public IEnumerable<Product> NewProduct { get; set; }


    }
    public class OrderFormViewModel
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
    public class ReviewFormViewModel
    {
        public Review Review { get; set; }
        public Product Product { get; set; }
    }

    public class ArticleDetailsViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
    public class AllArticleViewModel
    {
        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
    }
    public class MenuArticleViewModel
    {
        public int RootId { get; set; }
        public int CatId { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
    }
    public class ArticleCategoryViewModel
    {
        public ArticleCategory RootCategory { get; set; }
        public ArticleCategory Category { get; set; }
        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
    }
    public class SearchProductViewModel
    {
        public string Keywords { get; set; }
        public IPagedList<Product> Products { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public ProductCategory Category { get; set; }
        public int? CatId { get; set; }
        public int? BrandId { get; set; }
        public string Name { get; set; }
        public string Sort { get; set; }
        public int ProductResult { get; set; }
        public int BeginCount { get; set; }
        public int EndCount { get; set; }
    }

    public class LandingPageViewModel
    {
        public IEnumerable<Banner> Policy { get; set; }

        public IEnumerable<BannerLandingPage> Banners { get; set; }
        public IEnumerable<FeedbackProduct> Feedbacks { get; set; }
        public IEnumerable<Combo> Combos { get; set; }
        public IEnumerable<QaProduct> QaProducts { get; set; }

        public Product Product { get; set; }
        public LandingPage LandingPage { get; set; }    

    }
}


    

