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
    public class SubjectController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> Get(int id )
        {
            return await _context.Subjects.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> Post([FromBody] Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return Ok(subject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> Put(int id, [FromBody] Subject subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(subject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Subjects.FindAsync(id).Result == null)
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
            //TO-DO look for foreign keys

            Subject subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        
    }
}
