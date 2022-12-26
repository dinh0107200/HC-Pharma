using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC_Pharma.Models;

namespace HC_Pharma.ViewModel
{
    public class BannerViewModel
    {
        public Banner Banner { get; set; }
        public SelectList SelectGroup { get; set; }
        public BannerViewModel()
        {
            var listgroup = new Dictionary<int, string>
            {
                { 1, "Banner Chính (850 x 500)" },
                { 2, "Banner Phải (300 x 250)" },
                { 3, "Banner Phụ (500 x 250)" },
                { 4, "Banner Home Center (500 x 250)" },
                { 5, "Video" }
            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }
    public class ListBannerViewModel
    {
        public PagedList.IPagedList<Banner> Banners { get; set; }
        public int? GroupId { get; set; }
        public SelectList SelectGroup { get; set; }
        public ListBannerViewModel()
        {
            var listgroup = new Dictionary<int, string>
            {
                { 1, "Banner Chính (850 x 500)" },
                { 2, "Banner Phải (300 x 250)" },
                { 3, "Banner Phụ (500 x 250)" },
                { 4, "Banner Home Center (500 x 250)" },
                { 5, "Video" }
            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }
}