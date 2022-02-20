using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.StoreApi.Models.Entities
{
    public class OrderRowEntity
    {
       

        public OrderRowEntity()
        {

        }

        public OrderRowEntity( int orderId, int productId, int quantity, decimal price)
        {
           
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }


       


        [Key]
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderEntity Order { get; set; }      
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductEntity Products { get; set; }
        
        public int Quantity { get; set; }
        [Column(TypeName ="money")]
        public decimal Price { get; set; }

       

       
       

    }
}
