using System;
using System.ComponentModel.DataAnnotations;

namespace HC_Pharma.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm", Description = "Tên sản phẩm dài tối đa 200 ký tự")
            , Required(ErrorMessage = "Hãy nhập Tên sản phẩm"), StringLength(200, ErrorMessage = "Tối đa 200 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }

        [Display(Name = "Mô tả sản phẩm"), UIHint("TextArea")]
        public string Description { get; set; }
        [Display(Name = "Nhà sản xuất"), UIHint("TextBox")]
        public string Producer { get; set; }
        [Display(Name = "Xuất xứ"), UIHint("TextBox")]
        public string Origin { get; set; }
        [Display(Name = "Quy cách"), UIHint("TextBox")]
        public string Specifications { get; set; }
        [Display(Name = "Công dụng"), UIHint("EditorBox")]
        public string Uses { get; set; }

        [Display(Name = "Thành phần"), UIHint("EditorBox")]
        public string Body { get; set; }
        [Display(Name = "Hướng dẫn sử dụng"), UIHint("EditorBox")]
        public string Usermanual { get; set; }

        [Display(Name = "Danh sách ảnh"), UIHint("UploadMultiFile")]
        public string ListImage { get; set; }

        [Display(Name = "Giá niêm yết"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? PriceSale { get; set; }

        [Display(Name = "Sản phẩm mấy sao"), UIHint("NumberBox"), Range(1, 5, ErrorMessage = "Chỉ chọn từ 1 đến 5")]
        public int Star { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Ngày đăng")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự")
            , RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }

        [Display(Name = "Hiện trang chủ")]
        public bool Home { get; set; }

        //[Display(Name = "Sản phẩm nổi bật")]
        //public bool Hot { get; set; }

        [StringLength(300)]
        public string Url { get; set; }
        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }
        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMeta { get; set; }

        [Display(Name = "Danh mục sản phẩm"), Required(ErrorMessage = "Hãy chọn danh mục sản phẩm")]
        public int ProductCategoryId { get; set; }


        public virtual ProductCategory ProductCategory { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0} đ")]
        public decimal? FinalPrice => PriceSale ?? Price;

        public Product()
        {
            CreateDate = DateTime.Now;
            Active = true;
        }
    }

    public class LandingPage
    {
        public int Id { get; set; }
        [Display(Name = "Ảnh giới thiệu")]
        public string ImageIntro { get; set; }
        [Display(Name = "Giới thiệu"), UIHint("EditorBox")]
        public string Intro { get; set; }

        [Display(Name = "Ảnh giải pháp")]
        public string Imagesolution { get; set; }
        [Display(Name = "Giải pháp"), UIHint("EditorBox")]
        public string Solution { get; set; }


        [Display(Name = "Ảnh phân tích sản phẩm")]
        public string ImageAnalysis { get; set; }
        [Display(Name = "Phân tích sản phẩm"), UIHint("EditorBox")]
        public string ProductAnalysis { get; set; }
        [Display(Name = "Mô tả các dấu hiệu thường gặp + dấu hiệu"), UIHint("TextArea")]
        public string CommonDiseases { get; set; }
        [Display(Name = "Mô tả nguyên nhân gây bệnh"), UIHint("TextArea")]
        public string CauseDisease { get; set; }
        [Display(Name = "Mô tả câu hỏi thường gặp"), UIHint("TextArea")]
        public string QA { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class QaProduct
    {
        public int Id { get; set; }

        [Display(Name = "Tên câu hỏi")]
        public string Name { get; set; }
        [Display(Name = "Câu trả lời"), UIHint("TextArea")]
        public string Body { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự")
        ,RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
    public class Combo
    {
        public int Id { get; set; }
        [Display(Name = "Tên Combo"), Required(ErrorMessage = "Chưa nhập tên combo")]
        public string Name { get; set; }
        public int? ProductId { get; set; }
        [Display(Name = "Thứ tự")]
        public int Sort { get; set; }
        [Display(Name = "Giá niêm yết"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? PriceSale { get; set; }
        [Display(Name = "Ảnh sản phẩm")]
        public string Image { get; set; }
        public virtual Product Product { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
    }

    public class FeedbackProduct
    {
        public int Id { get; set; }
        [Display(Name = "Tên "), Required(ErrorMessage = "Chưa nhập tên")]
        public string Name { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Link video youtube")]
        public string UrlVideo { get; set; }
        [Display(Name = "File video mp4")]
        public string FileVideo { get; set; }
        [Display(Name = "Lời nhận xét"), StringLength(700, ErrorMessage = "Tối đa 700 ký tự"), UIHint("TextArea")]
        public string Content { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }

    public class BannerLandingPage
    {
        public int Id { get; set; }
        [Display(Name = "Tên "), Required(ErrorMessage = "Chưa nhập tên")]
        public string Name { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Lời nhận xét"), StringLength(700, ErrorMessage = "Tối đa 700 ký tự"), UIHint("TextArea")]
        public string Content { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public int ProductId { get; set; }
        public PostionAbout PostionAbout { get; set; }
        public virtual Product Product { get; set; }

    }
    public enum PostionAbout
    {
        [Display(Name = "Banner")]
        Banner = 1,
        [Display(Name = "Các bệnh thường gặp + dấu hiệu")]
        CommonDiseases,
        [Display(Name = "Nguyên nhân gây bệnh")]
        CauseDisease, 
    }
}