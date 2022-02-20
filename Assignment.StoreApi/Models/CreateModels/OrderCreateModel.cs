namespace Assignment.StoreApi.Models.CreateModels
{
    public class OrderCreateModel
    {
        public OrderCreateModel()
        {

        }
       

      
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }

       
    }
}
