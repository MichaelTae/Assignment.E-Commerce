using Assignment.MVC.Models.OrderRowModels;

namespace Assignment.MVC.Models.ViewModels
{
    public class OrderRowViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<OrderRowModel> orderRows { get; set; }

        public IEnumerable<ProductModel> product { get; set; } // implement?? 
    }
}
