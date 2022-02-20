namespace Assignment.StoreApi.Models
{
    public class OrderRowModel
    {
        public OrderRowModel()
        {

        }

        public OrderRowModel( int productId, int quantity, decimal price, string customerName, ProductModel product)
        {
           
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            CustomerName = customerName;           
            Product = product;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CustomerName { get; set; }
        public OrderModel Order { get; set; }       
        public ProductModel Product { get; set; }

       
    }
}
