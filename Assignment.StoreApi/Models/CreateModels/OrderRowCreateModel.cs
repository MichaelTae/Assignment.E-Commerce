namespace Assignment.StoreApi.Models.CreateModels
{
    public class OrderRowCreateModel
    {
        
       public int OrderId { get; set; } 
       
        public int ProductId { get; set; }
        public int Quantity { get; set; }
       
        
    }
}
