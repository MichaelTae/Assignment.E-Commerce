using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.StoreApi.Models.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {

        }


        public OrderEntity(int id,string customerName, DateTime orderCreated, int quantity, decimal totalPrice)
        {
            Id = id;
            CustomerName = customerName;
            OrderCreated = orderCreated;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }


        public OrderEntity(string customerName, DateTime orderCreated, int quantity, decimal totalPrice)
        {
            CustomerName = customerName;
            OrderCreated = orderCreated;            
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        [Key]
      
        public int Id { get; set; }
        [Required]     
        public string CustomerName { get; set; }

        [Required]
        public DateTime OrderCreated { get; set; }

        public DateTime OrderModified { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<OrderRowEntity> OrderRows { get; set; }
    }
}
