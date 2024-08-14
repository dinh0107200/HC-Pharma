using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HC_Pharma.Models;
using System.Drawing.Drawing2D;

namespace HC_Pharma.ViewModel
{
    public class InsertCategoryViewModel
    {
        public ProductCategory ProductCategory { get; set; }
    }
    public class ListProductViewModel
    {
        public PagedList.IPagedList<Product> Products { get; set; }
        public SelectList SelectCategories { get; set; }
        public SelectList ChildCategoryList { get; set; }
        public int? ParentId { get; set; }
        public int? CatId { get; set; }
        public string Name { get; set; }
        public string Sort { get; set; }

        public ListProductViewModel()
        {
            ChildCategoryList = new SelectList(new List<ProductCategory>(), "Id", "CategoryName");
        }
    }

    public class InsertProductViewModel
    {
        public Product Product { get; set; }
        [Display(Name = "Danh mục sản phẩm con"), Required(ErrorMessage = "Hãy chọn danh mục sản phẩm")]
        public int ParentId { get; set; }
        [Display(Name = "Danh mục sản phẩm cha")]
        public int? CategoryId { get; set; }
       
        public int CountFilter { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public SelectList SelectCategories { get; set; }
        public SelectList ChildCategoryList { get; set; }
        public SelectList ProductCategoryList { get; set; }

        [Display(Name = "Giá niêm yết"), UIHint("MoneyBox"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public string Price { get; set; }
        [Display(Name = "Giá khuyến mãi"), UIHint("MoneyBox"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public string PriceSale { get; set; }
        public InsertProductViewModel()
        {
            ChildCategoryList = new SelectList(new List<ProductCategory>(), "Id", "CategoryName");
        }
    }
    public class ListReviewViewModel
    {
        public PagedList.IPagedList<Review> Reviews { get; set; }
        public IEnumerable<Product> Products { get; set;}
        public string Name { get; set; }
    }

    public class InsertComboViewModel
    {
        public Combo Combo { get; set; }
        [Display(Name = "Giá niêm yết"), UIHint("MoneyBox"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public string Price { get; set; }
        [Display(Name = "Giá khuyến mãi"), UIHint("MoneyBox"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public string PriceSale { get; set; }
    }

    public class ListComboViewModel
    {
        public PagedList.IPagedList<Combo> Combos { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
    public class ListBannerLandingPageViewModel
    {
        public IEnumerable<BannerLandingPage> Banners { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
    }

}