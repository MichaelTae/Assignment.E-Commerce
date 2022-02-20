using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment.StoreApi.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class CategoryEntity
    {
        
        public CategoryEntity()
        {

        }
        public CategoryEntity(string name)
        {
            Name = name;
        }


        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<SubCategoryEntity> SubCategories { get; set; }

    }
}
