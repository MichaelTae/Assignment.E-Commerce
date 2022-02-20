using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment.StoreApi.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class SubCategoryEntity
    {
        public SubCategoryEntity()
        {

        }
        public SubCategoryEntity(string name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        
    }
}
