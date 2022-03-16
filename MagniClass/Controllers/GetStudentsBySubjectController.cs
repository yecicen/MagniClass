﻿using MagniClass.Data;
using MagniClass.Models;
using MagniClass.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagniClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetStudentsBySubjectController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public GetStudentsBySubjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<GetCoursesExtended>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CourseListVM>>> GetCourseList()
        //{
        //    var courseListVMs = new List<CourseListVM>();
        //    var courses = await _context.Courses.ToListAsync();

        //    foreach (var course in courses)
        //    {
        //        //get teacher count
        //        //get subjectsList
        //        //get average score
        //    }
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StudentListVM>>> GetAsync(int id)
        {
            List<StudentListVM> studentList = new List<StudentListVM>();

            var data = await _context.Students
                                .Join(_context.Grades,
                                  s => s.Id,
                                  g => g.StudentID,
                                  (s, g) => new
                                  {
                                      Grade = g.Score,
                                      StudentNumber = s.RegistrationNumber,
                                      SubjectId = g.SubjectID
                                  }).Where(g => g.SubjectId.Equals(id))
                                .ToListAsync();

            //var query = from s in _context.Students
            //            join g in _context.Grades on s.Id equals g.StudentID into gs
            //            from g in gs.DefaultIfEmpty()
            //            where g.SubjectID.Equals(id)
            //            select new { s.RegistrationNumber, g.Score };

            foreach (var item in data)
            {
                studentList.Add(new StudentListVM() { Grade = item.Grade, StudentNumber = item.StudentNumber });
            }
            return studentList;
        }
    }
}
