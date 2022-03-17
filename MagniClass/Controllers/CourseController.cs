using MagniClass.Data;
using MagniClass.Hubs;
using MagniClass.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagniClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        private readonly IHubContext<CourseHub> hubContext;

        public CourseController(ApplicationDbContext context, IHubContext<CourseHub> _hubContext)
        {
            _context = context;
            //hubContext = _hubContext; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id )
        {
            return await _context.Courses.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Post([FromBody] Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            //await hubContext.Clients.All.SendAsync("courseAdded", course);
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> Put(int id, [FromBody] Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                //await hubContext.Clients.All.SendAsync("courseAdded", course);
                return Ok(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Courses.FindAsync(id).Result == null)
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

            Course course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        
    }
}
