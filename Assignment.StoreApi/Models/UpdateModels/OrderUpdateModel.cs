namespace Assignment.StoreApi.Models.UpdateModels
{
    public class OrderUpdateModel
    {
       

        public string CustomerName { get; set; }

        public DateTime OrderModified { get; set; } = DateTime.Now;

        
    }
}
