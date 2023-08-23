using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class Agreement
    {
        [Key]
        [Required]
        public int Id{ get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "User Name")]
        public string? UserId{ get; set; }
        [ForeignKey("ProductGroup")]
        [Display(Name = "Product Group")]
        [Required]
        public int ProductGroupId{ get; set; }
        [ForeignKey("Product")]
        [Display(Name ="Product")]
        [Required]
        public int ProductId{ get; set; }
        [Display(Name = "Effective Date")]
        [Required]
        public DateTime EffectiveDate{ get; set; }
        [Display(Name = "Expiration Date")]
        [Required]
        public DateTime ExpirationDate{ get; set; }
        [Display(Name = "Product Price")]
        [Required]
        public decimal ProductPrice{ get; set; }
        [Display(Name = "New Price")]
        [Required]
        public decimal NewPrice{ get; set; }
        
        [Required]
        public bool Active { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public ProductGroup? ProductGroup { get; set; }
        public Product? Product { get; set; }

    }
}
