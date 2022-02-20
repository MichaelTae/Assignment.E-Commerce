using Assignment.StoreApi.Models;

namespace Assignment.MVC.Models.OrderModels
{
    public class OrderModel
    {
        public int Id { get; set; }      
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
