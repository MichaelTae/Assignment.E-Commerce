using Assignment.MVC.Models.OrderModels;

namespace Assignment.MVC.Models.OrderRowModels
{
    public class OrderRowModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public OrderModel OrderModel { get; set; }
        public ProductModel ProductModel { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
