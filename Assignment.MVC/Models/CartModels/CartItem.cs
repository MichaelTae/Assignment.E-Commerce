namespace Assignment.MVC.Models.CartModels
{
    public class CartItem
    {
        public CartItem()
        {

        }
       
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
