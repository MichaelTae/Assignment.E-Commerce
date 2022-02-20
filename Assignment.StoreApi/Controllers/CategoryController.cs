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
    public class CategoryController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CategoryController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync()
        {
            var categories = new List<CategoryModel>();
            foreach (var item in await _context.Categories.ToListAsync())
            {
                categories.Add(new CategoryModel(item.Id, item.Name));
            }
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);

            if (categoryEntity == null)
            {
                return NotFound();
            }

            return Ok(new CategoryModel(categoryEntity.Id, categoryEntity.Name));
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryAsync(int id, UpdateCategoryModel model)
        {
            if (ModelState.IsValid || CategoryEntityExists(id))
            {


                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;

                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategoryAsync(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var createCategory = new CategoryEntity(model.Name);
                _context.Categories.Add(createCategory);
                await _context.SaveChangesAsync();

                var newCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == createCategory.Id);

                return CreatedAtAction("GetCategory", new { id = createCategory.Id }, new CategoryModel(newCategory.Id, newCategory.Name));
            }
            return BadRequest();



        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
