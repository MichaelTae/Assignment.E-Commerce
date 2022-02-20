

namespace Assignment.MVC.Models
{
    public class ProductModel
    {
        public ProductModel()
        {

        }


        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    
        public string SubCategoryName { get; set; }
        
        public string CategoryName { get; set; }
        

    }
}
