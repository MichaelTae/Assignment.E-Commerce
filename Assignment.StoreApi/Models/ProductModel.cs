namespace Assignment.StoreApi.Models
{
    public class ProductModel
    {

        public ProductModel()
        {

        }

        


        public ProductModel(int id, string name, string description, decimal price, int subCategoryId, string subCategoryName, string categoryName)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            SubCategoryId = subCategoryId;
            CategoryName = categoryName;
            SubCategoryName = subCategoryName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

    }
}
