using MagniClass.Data;
using MagniClass.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MagniClass.Controllers
{
    public class AddSubjectsToCourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddSubjectsToCourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<CourseSubjects>> Post([FromBody] CourseSubjects[] courseSubjects)
        {

            await _context.CoursesSubjects.AddRangeAsync(courseSubjects);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
