using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HC_Pharma.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Display(Name = "Họ tên"), Required(ErrorMessage = "Hãy nhập họ tên"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Customer { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Nhận xét"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string Coment { get; set; }

        [Display(Name = "Đánh giá")]
        public int Rate { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Phê duyệt")]
        public bool Active { get; set; }
        public virtual Product Product { get; set; }
        public Review()
        {
            Active= false;
        }
    }
}