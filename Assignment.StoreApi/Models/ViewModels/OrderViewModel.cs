namespace Assignment.StoreApi.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {

        }




        public int Id { get; set; }
       
        public string CustomerName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public decimal TotalPrice { get; set; }


        public List<OrderRowModelObject> OrderRows { get; set; }

       

        
    }
}
