using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string GroupDescription { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string GroupCode { get; set; }
        public bool Active { get; set; }

        //public ICollection<Product> Products { get; set; }
        //public ICollection<Agreement> Agreement { get; set; }
    }
}
