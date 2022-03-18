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
    public class GetSubjectsByCourseController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public GetSubjectsByCourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectListVM>>> GetAsync()
        {
            List<SubjectListVM> subjectList = new List<SubjectListVM>();
            int studentCount = 0;
            Teacher teacher = new Teacher();
            double subjectAverage = 0;
            var data = await _context.Subjects.ToListAsync();
            foreach (var item in data)
            {
                teacher = await _context.Teachers.FindAsync(item.TeacherID);
                studentCount = await _context.Students
                                    .Join(_context.SubjectStudents,
                                      s => s.Id,
                                      c => c.StudentId,
                                      (s, c) => new
                                      {
                                          SubjectId = c.SubjectId
                                      }).Where(x => x.SubjectId.Equals(item.Id)).CountAsync();

                subjectAverage =  await _context.Students
                                        .Join(_context.Grades,
                                          s => s.Id,
                                          c => c.StudentID,
                                          (s, c) => new
                                          {
                                              SubjectId = c.SubjectID,
                                              Grade = c.Score
                                          }).Where(x => x.SubjectId.Equals(item.Id))
                                          .AverageAsync((x => x.Grade));
                subjectList.Add(
                    new SubjectListVM() { 
                        SubjectId = item.Id,
                        TeacherId = teacher.Id,
                        NumberOfStudents = studentCount,
                        SubjectAvg = Math.Round(subjectAverage, 2),
                        SubjectName = item.Name,
                        TeacherName = teacher.Name
                    });

            }
            return subjectList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SubjectListVM>>> GetAsync(int id)
        {
            List<SubjectListVM> subjectList = new List<SubjectListVM>();

            var data = await _context.Subjects
                                .Join(_context.CoursesSubjects,
                                  s => s.Id,
                                  c => c.SubjectId,
                                  (s, c) => new
                                  {
                                      SubjectId = s.Id,
                                      TeacherId = s.TeacherID,
                                      SubjectName = s.Name,
                                      CourseId = c.CourseId
                                  }).Where(x => x.CourseId.Equals(id))
                                .ToListAsync();

            foreach (var item in data)
            {
                Teacher teacher = await _context.Teachers.FindAsync(item.TeacherId);
                int studentCount = await _context.Students
                                    .Join(_context.SubjectStudents,
                                      s => s.Id,
                                      c => c.StudentId,
                                      (s, c) => new
                                      {
                                          SubjectId = c.SubjectId
                                      }).Where(x => x.SubjectId.Equals(item.SubjectId)).CountAsync();

                double subjectAverage = await _context.Students
                                        .Join(_context.Grades,
                                          s => s.Id,
                                          c => c.StudentID,
                                          (s, c) => new
                                          {
                                              SubjectId = c.SubjectID,
                                              Grade = c.Score
                                          }).Where(x => x.SubjectId.Equals(item.SubjectId))
                                          .AverageAsync((x => x.Grade));
                subjectList.Add(
                    new SubjectListVM()
                    {
                        SubjectId = item.SubjectId,
                        TeacherId = teacher.Id,
                        NumberOfStudents = studentCount,
                        SubjectAvg = Math.Round(subjectAverage, 2),
                        SubjectName = item.SubjectName,
                        TeacherName = teacher.Name
                    });

            }
            return subjectList;
        }
    }
}
