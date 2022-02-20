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
    public class SubCategoryController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public SubCategoryController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/SubCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryModel>>> GetSubCategoriesAsync()
        {
            var categories = new List<SubCategoryModel>();
            foreach(var item in await _context.SubCategories.ToListAsync())
            {
                categories.Add(new SubCategoryModel(item.Id, item.Name, item.CategoryId)); 
            }
            return categories;
        }

        // GET: api/SubCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryModel>> GetSubCategoryAsync(int id)
        {
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x=>x.Id == id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return new SubCategoryModel(subCategory.Id, subCategory.Name, subCategory.CategoryId);
        }

        // PUT: api/SubCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategoryEntity(int id, UpdateSubCategoryModel model)
        {
            if (!ModelState.IsValid || !SubCategoryEntityExists(id))
            {
                return BadRequest();
            }
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
                return NotFound();
            
            subCategory.Name = model.Name;
            subCategory.CategoryId = model.categoryId;


            _context.Entry(subCategory).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            return Ok();

           
        }

        // POST: api/SubCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategoryEntity>> CreateSubCategoryAsync(CreateSubCategoryModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

                var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x=>x.Name == model.Name);
                if(subCategory != null) 
                
                return BadRequest("SubCategory Already Exists");

                    var createSubCategory = new SubCategoryEntity(model.Name, model.CategoryId);
                    _context.SubCategories.Add(createSubCategory);
                    await _context.SaveChangesAsync();

                    var newSubCategory = await _context.SubCategories.FirstOrDefaultAsync(x=>x.Id == createSubCategory.Id);
                    return CreatedAtAction("GetSubCategory", new { id = newSubCategory.Id }, new SubCategoryModel(newSubCategory.Id, newSubCategory.Name, newSubCategory.CategoryId));
                
            
            
           
        }

        // DELETE: api/SubCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryAsync(int id)
        {
            var subCategoryEntity = await _context.SubCategories.FindAsync(id);
            if (subCategoryEntity == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryEntityExists(int id)
        {
            return _context.SubCategories.Any(e => e.Id == id);
        }
    }
}
