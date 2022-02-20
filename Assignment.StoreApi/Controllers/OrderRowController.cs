#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.StoreApi.Models.Entities;
using Assignment.StoreApi.Models;
using Assignment.StoreApi.Models.CreateModels;
using Assignment.StoreApi.Models.UpdateModels;
using Assignment.StoreApi.Models.ViewModels;
using Assignment.StoreApi.Filters;

namespace Assignment.StoreApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderRowController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public OrderRowController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderRow By OrderRow ID /Get All
        [HttpGet("id")]
        public async Task<ActionResult<OrderRowsViewModel>> GetOrderRowEntity(int id)
        {
            var list = new List<OrderRowModelObject>();
            var orderRowModelObject = new OrderRowsViewModel();
            bool suppliedId = false;
            if (id > 0)
            {
                suppliedId = true;  
            }

            if (suppliedId == true)
            {
                
                foreach (var item in await _context.OrderRows.Include(x => x.Products).Include(y => y.Order).Where(x=> x.Id == id).ToListAsync())
                {
                    list.Add(new OrderRowModelObject(item.Id,
                        item.OrderId,
                        item.ProductId,
                        item.Quantity,
                        item.Products.Name,
                        item.Products.Price));
                }
                orderRowModelObject.OrderRowObject = list;
                return orderRowModelObject;

            }

            
            foreach (var item in await _context.OrderRows.Include(x => x.Products).Include(y => y.Order).ToListAsync())
            {
                list.Add(new OrderRowModelObject(item.Id,
                    item.OrderId,
                    item.ProductId,
                    item.Quantity,
                    item.Products.Name,
                    item.Products.Price));
            }
            orderRowModelObject.OrderRowObject = list;
            return orderRowModelObject;

           
        }

        // GET: api/OrderRow/5 // Get by OrderId
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRowsViewModel>> GetOrderRowByOrderIdEntity(int id)
        {
            var orderRow =  await _context.OrderRows.Include(x=>x.Products).Include(x=>x.Order).Where(x=> x.OrderId == id).ToListAsync();
            var orderRowModelObject = new OrderRowsViewModel();
            
            if (orderRow == null)
            {
                return NotFound();
            }
            var productsRowList = new List<OrderRowModelObject>();
            foreach (var item in orderRow)
            {
                
                productsRowList.Add(new OrderRowModelObject(
                    item.Id,
                    item.OrderId,
                    item.ProductId,
                    item.Quantity,                                     
                    item.Products.Name,
                    item.Products.Price
                    )); 

            }
          
            orderRowModelObject.OrderRowObject = productsRowList;
            return orderRowModelObject;
        }

        // PUT: api/OrderRow/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderRowEntity(int id, OrderRowUpdateModel rowUpdateModel)
        {


            if (!OrderRowEntityExists(id))
            {
                return BadRequest();
            }
            var rowEntity = await _context.OrderRows.FindAsync(id);
            if(rowEntity == null)
            {
                return BadRequest();
            }
            
            rowEntity.OrderId = rowUpdateModel.OrderId;
            rowEntity.ProductId = rowUpdateModel.ProductId;
            rowEntity.Quantity = rowUpdateModel.Quantity;
 
            _context.Entry(rowEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderRow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderRowModel>> PostOrderRowEntity(OrderRowCreateModel orderRow)
        {
            if (ModelState.IsValid)
            {
                decimal prodprice = 0;
                var orders = await _context.Orders.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                var productPrice = await _context.Products.Where(y => y.Id == orderRow.ProductId).FirstOrDefaultAsync();
                prodprice = productPrice.Price;
                

                var rowEntity = new OrderRowEntity(orders.Id, orderRow.ProductId, orderRow.Quantity, prodprice);

                _context.OrderRows.Add(rowEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest();


            
        }


        private bool OrderRowEntityExists(int id)
        {
            return _context.OrderRows.Any(e => e.OrderId == id);
        }
    }
}
