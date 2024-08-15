using HC_Pharma.Models;
using System.Collections.Generic;
using System.Web.Mvc;

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
                { 0, "Banner Slides (1900 x 650)" },
                {1, "Đối tác (300 x 150)" },
                {2, "Chính sách trong LandingPage" }

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
                {0, "Banner Slides (1900 x 650)" },
                { 1, "Đối tác (300 x 150)" },
                 {2, "Chính sách trong LandingPage" }
            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }
}