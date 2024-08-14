using System.ComponentModel.DataAnnotations;

namespace HC_Pharma.Models
{
    public class Cart 
    { 
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int? ComboId { get; set; }

        public decimal? Price { get; set; } 
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; } 
        public virtual Product Product { get; set; }
        public virtual Combo Combo { get; set; }

    }
} 