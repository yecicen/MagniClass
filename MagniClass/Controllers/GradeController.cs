using MagniClass.Data;
using MagniClass.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagniClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetAsync()
        {
            return await _context.Grades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> Get(int id )
        {
            return await _context.Grades.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> Post([FromBody] Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok(grade);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Grade>> Put(int id, [FromBody] Grade grade)
        {
            if (id != grade.Id)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(grade);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Grades.FindAsync(id).Result == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Grade grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        
    }
}
