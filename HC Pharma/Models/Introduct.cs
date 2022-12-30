using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HC_Pharma.Models
{
    public class Introduct
    {
        public int Id { get; set; }
        [Display(Name = "Slogan"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextBox")]
        public string Slogan { get; set; }
        [Display(Name = "Hình ảnh"), StringLength(500)]
        public string Image { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên"), UIHint("NumberBox")]
        public int Sort { get; set; }
    }
}