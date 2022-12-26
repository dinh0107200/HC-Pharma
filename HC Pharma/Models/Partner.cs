using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HC_Pharma.Models
{
    public class Partner
    {
        public int Id { get; set; }
        [Display(Name = "Tên đối tác"), Required(ErrorMessage = "Hãy nhập họ tên"), UIHint("TextBox"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Name { get; set; }
        [Display(Name = "Ảnh đại diện"), StringLength(500)]
        public string Image { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        public Partner()
        {
            Active = true;
        }
    }
}