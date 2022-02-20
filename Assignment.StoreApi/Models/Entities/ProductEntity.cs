using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.StoreApi.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class ProductEntity
    {
        public ProductEntity()
        {

        }
        public ProductEntity(string name, string description, decimal price, int subCategoryId)
        {            
            Name = name;
            Description = description;
            Price = price;
            SubCategoryId = subCategoryId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName="money")]
        public decimal Price { get; set; }
        [Required]
        public int SubCategoryId { get; set; }

        public SubCategoryEntity SubCategory { get; set; }

      
    }
}
