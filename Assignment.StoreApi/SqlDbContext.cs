using Assignment.StoreApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.StoreApi
{
    public class SqlDbContext :DbContext
    {
        public SqlDbContext()
        {

        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {


        }
    
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }

        public virtual DbSet<SubCategoryEntity> SubCategories { get; set; }

        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderRowEntity> OrderRows { get; set;}


        
    
    
    
    }
}
