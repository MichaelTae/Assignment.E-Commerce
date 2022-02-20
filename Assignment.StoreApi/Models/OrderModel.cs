namespace Assignment.StoreApi.Models
{
    public class OrderModel
    {
        public OrderModel()
        {

        }
       
        public OrderModel(int id,  string customerName, DateTime orderCreated, int quantity, decimal price)
        {
            Id = id;                      
            CustomerName = customerName; 
            OrderCreated = orderCreated;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; set; }
        
        public string CustomerName { get; set; }
 
        public DateTime OrderCreated { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }


       
    }
}
