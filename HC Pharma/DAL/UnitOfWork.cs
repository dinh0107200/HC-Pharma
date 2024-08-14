using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HC_Pharma.Models;
using Microsoft.Ajax.Utilities;

namespace HC_Pharma.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly DataEntities _context = new DataEntities();
        private GenericRepository<Admin> _adminRepository;
        private GenericRepository<ArticleCategory> _articategoryRepository;
        private GenericRepository<Article> _articleRepository;
        private GenericRepository<Banner> _bannerRepository;
        private GenericRepository<Contact> _contactRepository;
        private GenericRepository<ConfigSite> _configRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductCategory> _productCategoryRepository;
        private GenericRepository<Review> _reviewRepository;
        private GenericRepository<Feedback> _feedbackRepository;
        private GenericRepository<Partner> _partnerRepository;
        private GenericRepository<Introduct> _introductRepository;
        private GenericRepository<Cart> _cartRepository;
        private GenericRepository<City> _cityRepository;
        private GenericRepository<District> _districtRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;
        private GenericRepository<Ward> _wardRepository;
        private GenericRepository<Combo> _comboRepository;
        private GenericRepository<FeedbackProduct> _feedbackProductRepository;
        private GenericRepository<LandingPage> _landingPageRepository;
        private GenericRepository<QaProduct> _qaProductRepository;
        private GenericRepository<BannerLandingPage> _bannerLandingPageRepository;


        public GenericRepository<BannerLandingPage> BannerLandingPageRepository =>
        _bannerLandingPageRepository ?? (_bannerLandingPageRepository = new GenericRepository<BannerLandingPage>(_context));
        public GenericRepository<QaProduct> QaProductRepository =>
        _qaProductRepository ?? (_qaProductRepository = new GenericRepository<QaProduct>(_context));
        public GenericRepository<LandingPage> LandingPageRepository =>
        _landingPageRepository ?? (_landingPageRepository = new GenericRepository<LandingPage>(_context));
        public GenericRepository<FeedbackProduct> FeedbackProductRepository =>
        _feedbackProductRepository ?? (_feedbackProductRepository = new GenericRepository<FeedbackProduct>(_context));
        public GenericRepository<Combo> ComboRepository =>
          _comboRepository ?? (_comboRepository = new GenericRepository<Combo>(_context));
        public GenericRepository<City> CityRepository =>
            _cityRepository ?? (_cityRepository = new GenericRepository<City>(_context));
        public GenericRepository<District> DistrictRepository =>
            _districtRepository ?? (_districtRepository = new GenericRepository<District>(_context));
        public GenericRepository<Order> OrderRepository =>
            _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_context));
        public GenericRepository<OrderDetail> OrderDetailRepository =>
            _orderDetailRepository ?? (_orderDetailRepository = new GenericRepository<OrderDetail>(_context));
        public GenericRepository<Ward> WardRepository =>
            _wardRepository ?? (_wardRepository = new GenericRepository<Ward>(_context));
        public GenericRepository<Cart> CartRepository =>
            _cartRepository ?? (_cartRepository = new GenericRepository<Cart>(_context));
        public GenericRepository<Introduct> _IntroductRepository =>
            _introductRepository ?? (_introductRepository = new GenericRepository<Introduct>(_context));
        public GenericRepository<Partner> PartnerRepository =>
           _partnerRepository ?? (_partnerRepository = new GenericRepository<Partner>(_context));
        public GenericRepository<Review> ReviewRepository =>
            _reviewRepository ?? (_reviewRepository = new GenericRepository<Review>(_context));
        public GenericRepository<Feedback> FeedbackRepository =>
            _feedbackRepository ?? (_feedbackRepository = new GenericRepository<Feedback>(_context));
        
        public GenericRepository<Product> ProductRepository =>
            _productRepository ?? (_productRepository = new GenericRepository<Product>(_context));
        public GenericRepository<ProductCategory> ProductCategoryRepository =>
            _productCategoryRepository ?? (_productCategoryRepository = new GenericRepository<ProductCategory>(_context));
        public GenericRepository<ConfigSite> ConfigSiteRepository =>
            _configRepository ?? (_configRepository = new GenericRepository<ConfigSite>(_context));
        public GenericRepository<Contact> ContactRepository =>
            _contactRepository ?? (_contactRepository = new GenericRepository<Contact>(_context));
        public GenericRepository<Banner> BannerRepository =>
            _bannerRepository ?? (_bannerRepository = new GenericRepository<Banner>(_context));
        public GenericRepository<Article> ArticleRepository =>
            _articleRepository ?? (_articleRepository = new GenericRepository<Article>(_context));
        public GenericRepository<ArticleCategory> ArticleCategoryRepository =>
            _articategoryRepository ?? (_articategoryRepository = new GenericRepository<ArticleCategory>(_context));
        public GenericRepository<Admin> AdminRepository =>
            _adminRepository ?? (_adminRepository = new GenericRepository<Admin>(_context));
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}