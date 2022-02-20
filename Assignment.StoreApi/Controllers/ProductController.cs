#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.StoreApi.Models.Entities;
using Assignment.StoreApi.Models;
using Assignment.StoreApi.Models.UpdateModels;
using Assignment.StoreApi.Models.CreateModels;
using Assignment.StoreApi.Filters;

namespace Assignment.StoreApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public ProductController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/Product/subCategory")] 
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsBySubCategoryAsync(string subCategory)
        {            
                var products = new List<ProductModel>();
                foreach (var item in await _context.Products.Include(x => x.SubCategory).ThenInclude(y => y.Category).Where(x => x.SubCategory.Name == subCategory).ToListAsync())
                {
                    products.Add(
                        new ProductModel(
                        item.Id, 
                        item.Name, 
                        item.Description,
                        item.Price, 
                        item.SubCategoryId,
                        item.SubCategory.Name,
                        item.SubCategory.Category.Name));
                }
                return products;
            

        }
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsAsync()
        {
           
                var products = new List<ProductModel>();
                foreach (var item in await _context.Products.Include(x => x.SubCategory).ThenInclude(y => y.Category).ToListAsync())
                {
                    products.Add(
                        new ProductModel(
                        item.Id, 
                        item.Name, 
                        item.Description, 
                        item.Price, 
                        item.SubCategoryId, 
                        item.SubCategory.Name, 
                        item.SubCategory.Category.Name));
                }
                return products;
         

        }

        //GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductEntity(int id)
        {
            var product = await _context.Products.Include(x => x.SubCategory).ThenInclude(y => y.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
                
            
            return new ProductModel(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.SubCategoryId,
                product.SubCategory.Name,
                product.SubCategory.Category.Name
                
                );

        }
        

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductModel model)
        {
            var updatedProduct = await _context.Products.FindAsync(id);
            if (!ModelState.IsValid || !ProductEntityExists(id))
            {
                return BadRequest();
            }
           
            updatedProduct.Name = model.Name;
            updatedProduct.Description = model.Description;
            updatedProduct.Price = model.Price;
            updatedProduct.SubCategoryId = model.SubCategoryId;
            
            
            _context.Entry(updatedProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();

            

           
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductEntity>> CreateProductAsync(CreateProductModel model)
        {
            if (ModelState.IsValid)
            {
                
                var createdProduct = new ProductEntity(model.Name,model.Description,model.Price,model.SubCategoryId);
                _context.Products.Add(createdProduct);
                await _context.SaveChangesAsync();

               
                var newProduct = await _context.Products.Include(x=> x.SubCategory.Category).FirstOrDefaultAsync(x=> x.Id == createdProduct.Id);

                return CreatedAtAction("GetProductEntity", new {id = createdProduct.Id}, new ProductModel(
                    newProduct.Id, 
                    newProduct.Name,
                    newProduct.Description, 
                    newProduct.Price, 
                    newProduct.SubCategoryId,
                    newProduct.SubCategory.Name,
                    newProduct.SubCategory.Category.Name
                    ));
            }

            return BadRequest();

           
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
