using AuthSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [ForeignKey("ProductGroup")]
        public int ProductGroupId { get; set; }
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product #")]
        public string ProductNumber { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public ProductGroup ProductGroup { get; set; }
    }
}
