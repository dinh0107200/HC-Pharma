﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC_Pharma.Models
{
    public class ConfigSite
    {
        public int Id { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Facebook"),
         Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Facebook { get; set; }

        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Youtube"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Youtube { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Twitter"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Twitter { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Instagram"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Instagram { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Skype"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Skype { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Google Plus"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string GooglePlus { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Pinterest"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Pinterest { get; set; }
        [StringLength(4000, ErrorMessage = "Tối đa 4000 ký tự"), Display(Name = "Mã nhúng Live chat"),
        UIHint("TextArea")]
        public string LiveChat { get; set; }
        [Display(Name = "Logo"), UIHint("ImageConfig")]
        public string Image { get; set; }
        [Display(Name = "Favicon"), UIHint("ImageConfig")]
        public string Favicon { get; set; }
        [StringLength(4000, ErrorMessage = "Tối đa 4000 ký tự"), Display(Name = "Mã Google Map"), UIHint("TextArea")]
        public string GoogleMap { get; set; }
        [StringLength(4000, ErrorMessage = "Tối đa 4000 ký tự"), Display(Name = "Mã Google Analytics"), UIHint("TextArea")]
        public string GoogleAnalytics { get; set; }
        [Display(Name = "Địa chỉ"), UIHint("TextBox")]
        public string Place { get; set; }
        [Display(Name = "Thẻ title"), StringLength(200, ErrorMessage = "Tối đa 200 ký tự"), UIHint("TextBox")]
        public string Title { get; set; }
        [Display(Name = "Ảnh giới thiệu"), UIHint("ImageAbout")]
        public string AboutImage { get; set; }
        [Display(Name = "Thông tin bài giới thiệu"), UIHint("EditorBox")]
        public string AboutText { get; set; }
        [Display(Name = "Đường dẫn Bài giới thiệu"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextBox")]
        public string AboutUrl { get; set; }
        [Display(Name = "Thẻ description"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string Description { get; set; }
        [Display(Name = "Hotline"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), UIHint("TextBox")]
        public string Hotline { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Tài khoản Zalo"),
       Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Zalo { get; set; }
        [Display(Name = "Chat Messenger"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), UIHint("TextBox")]
        public string Mess { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Tiki"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Tiki { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Tik Tok"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Tiktok { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Shopee"),
        Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Shopee { get; set; }
        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Lazada"),
       Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Lazada { get; set; }
        [StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), Display(Name = "Email"), UIHint("TextBox")]
        public string Email { get; set; }
        [Display(Name = "Thông tin liên hệ"), UIHint("EditorBox")]
        public string InfoContact { get; set; }
        [Display(Name = "Thông tin chân trang"), UIHint("EditorBox")]
        public string InfoFooter { get; set; }
        [Display(Name = "Tài khoản ngân hàng"), UIHint("EditorBox")]
        public string BankInfo { get; set; }

    }
}