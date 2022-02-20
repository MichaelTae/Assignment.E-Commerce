namespace Assignment.StoreApi.Models
{
    public class OrderRowModelObject
    {
        public OrderRowModelObject()
        {

        }

       

        public OrderRowModelObject(int id, int orderId, int productId, int quantity, string productName, decimal productPrice)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;                      
            ProductName = productName;
            ProductPrice = productPrice;
        }


        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }     
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        
       
    }
}
