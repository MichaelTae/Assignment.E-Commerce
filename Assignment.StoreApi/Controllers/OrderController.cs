#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.StoreApi.Models.Entities;
using Assignment.StoreApi.Models;
using Assignment.StoreApi.Models.ViewModels;
using Assignment.StoreApi.Models.CreateModels;
using Assignment.StoreApi.Models.UpdateModels;
using Assignment.StoreApi.Filters;

namespace Assignment.StoreApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public OrderController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrders()
        {

            var listOfOrders = new List<OrderViewModel>();            

            var orders = await _context.Orders.ToListAsync();
            foreach (var item in orders)
            {
                var orderModel = new OrderViewModel();
                orderModel.OrderRows = new List<OrderRowModelObject>();
                orderModel.Id = item.Id;
                orderModel.CustomerName = item.CustomerName;
                orderModel.DateCreated = item.OrderCreated;
                orderModel.DateUpdated = item.OrderModified;

             
                var orderRows = await _context.OrderRows.Include(x=>x.Products).Where(x => x.OrderId == orderModel.Id).ToListAsync();

                foreach(var t in orderRows)
                {

                    orderModel.TotalPrice += t.Price * t.Quantity;
                    orderModel.OrderRows.Add(new OrderRowModelObject(t.Id,t.OrderId,t.ProductId,t.Quantity,t.Products.Name,t.Price));
                    
                    

                }

                listOfOrders.Add(orderModel);
                
                
                



            }

            



            return listOfOrders;

        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrderEntity(int id)
        {

            var listOfOrders = new List<OrderViewModel>();

            var orders = await _context.Orders.Where(x=>x.Id == id).ToListAsync();
            foreach (var item in orders)
            {
                var orderModel = new OrderViewModel();
                orderModel.OrderRows = new List<OrderRowModelObject>();
                orderModel.Id = item.Id;
                orderModel.CustomerName = item.CustomerName;
                orderModel.DateCreated = item.OrderCreated;
                orderModel.DateUpdated = item.OrderModified;


                var orderRows = await _context.OrderRows.Include(x => x.Products).Where(x => x.OrderId == orderModel.Id).ToListAsync();

                foreach (var t in orderRows)
                {

                    orderModel.TotalPrice += t.Price * t.Quantity;
                    orderModel.OrderRows.Add(new OrderRowModelObject(t.Id, t.OrderId, t.ProductId, t.Quantity, t.Products.Name, t.Price));



                }

                listOfOrders.Add(orderModel);






            }





            return listOfOrders;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderEntity(int id, OrderUpdateModel model)
        {
          
            var orderEntity = await _context.Orders.FindAsync(id);
          
            if (!OrderEntityExists(id))
            {
                return BadRequest();
            }
            orderEntity.CustomerName = model.CustomerName;
            orderEntity.OrderModified = model.OrderModified;
            _context.Entry(orderEntity).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderEntity(OrderCreateModel model)
        {

            var order = new OrderEntity(model.CustomerName, model.DateCreated, model.Quantity, model.TotalPrice);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderEntity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);




            return CreatedAtAction("GetOrderEntity", new { id = orderEntity.Id }, new OrderModel(
                orderEntity.Id,
                orderEntity.CustomerName,
                orderEntity.OrderCreated,
                orderEntity.Quantity,
                orderEntity.TotalPrice));






        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderEntity(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
