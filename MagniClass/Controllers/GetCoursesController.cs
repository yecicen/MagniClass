using MagniClass.Data;
using MagniClass.Models;
using MagniClass.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagniClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCoursesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public GetCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseListVM>>> GetAsync()
        {
            List<CourseListVM> courseList = new List<CourseListVM>();

            var data = await _context.Courses.ToListAsync();
            //for each course, get subjects -> teacherCount
            //for each course, get subjects -> grades -> avg + count
            foreach (var course in data)
            {
                double gradeSum = 0;
                int studentCountSum = 0;
                var subjects = await _context.Courses
                                .Join(_context.CoursesSubjects,
                                  c => c.Id,
                                  s => s.CourseId,
                                  (c, s) => new
                                  {
                                      SubjectId = s.SubjectId,
                                      CourseId = c.Id
                                  }).Where(x => x.CourseId.Equals(course.Id)).ToListAsync();
                foreach (var subject in subjects)
                {
                    var subjectGrades = await _context.Students
                        .Join(_context.Grades,
                          s => s.Id,
                          c => c.StudentID,
                          (s, c) => new
                          {
                              SubjectId = c.SubjectID,
                              Grade = c.Score,
                              StudentId = c.StudentID
                          }).Where(x => x.SubjectId.Equals(subject.SubjectId)).ToListAsync();
                    gradeSum += subjectGrades.Sum(x => x.Grade);
                    studentCountSum += subjectGrades.Count;
                }
                courseList.Add(new CourseListVM()
                {
                    CourseAvg = Math.Round(gradeSum / studentCountSum, 2),
                    Name = course.Name,
                    NumberOfStudents = studentCountSum,
                    NumberOfTeachers = subjects.Count
                }); ;
            }
            return courseList;
        }
    }
}