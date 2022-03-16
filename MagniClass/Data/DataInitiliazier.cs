using MagniClass.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MagniClass.Data
{
    public static class DataInitiliazier 
    {
        public async static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var courses = new List<Course>()
                {
                    new Course(){ Name = "Computer Science"},
                    new Course(){ Name = "Data Science"},
                    new Course(){ Name = "Web Development"}
                };
                courses.ForEach(s => context.Courses.Add(s));
                await context.SaveChangesAsync();

                var teachers = new List<Teacher>()
                {
                    new Teacher(){ Name = "Denise Ritchie", Salary = 50000, Birthday = DateTime.Parse("1941-09-09")},
                    new Teacher(){ Name = "Bjarne Stroustrup", Salary = 48000, Birthday = DateTime.Parse("1950-12-30")},
                    new Teacher(){ Name = "Ian Sommerville", Salary = 45000, Birthday = DateTime.Parse("1951-02-23")},

                };

                teachers.ForEach(s => context.Teachers.Add(s));
                await context.SaveChangesAsync();

                var subjects = new List<Subject>()
                {
                    new Subject(){ Name = "Introduction to Programming", TeacherID = 1},
                    new Subject(){ Name = "Object Oriented Programming", TeacherID = 2},
                    new Subject(){ Name = "Introduction to Software Engineering", TeacherID = 3},
                    new Subject(){ Name = "Introduction to Web Technologies", TeacherID = 3},
                };

                subjects.ForEach(s => context.Subjects.Add(s));
                await context.SaveChangesAsync();

                var courseSubjects = new List<CourseSubjects>()
                {
                    new CourseSubjects(){CourseId = 1, SubjectId = 1},
                    new CourseSubjects(){CourseId = 1, SubjectId = 2},
                    new CourseSubjects(){CourseId = 2, SubjectId = 2},
                    new CourseSubjects(){CourseId = 2, SubjectId = 3},
                    new CourseSubjects(){CourseId = 3, SubjectId = 4},
                };

                courseSubjects.ForEach(s => context.CoursesSubjects.Add(s));
                await context.SaveChangesAsync();

                var students = new List<Student>()
                {
                    new Student(){ Name= "Yunus", Birthday = DateTime.Parse("1994-11-16"), RegistrationNumber = "202094",  },
                    new Student(){ Name= "Jack", Birthday = DateTime.Parse("1994-11-16"), RegistrationNumber = "202094",  },
                    new Student(){ Name= "Amy", Birthday = DateTime.Parse("1994-11-16"), RegistrationNumber = "202094",  },
                    new Student(){ Name= "Pedro", Birthday = DateTime.Parse("1994-11-16"), RegistrationNumber = "202094",  },
                    new Student(){ Name= "Xiu", Birthday = DateTime.Parse("1994-11-16"), RegistrationNumber = "202094",  },
                    new Student(){ Name= "Ruth", Birthday = DateTime.Parse("1994-11-16"), RegistrationNumber = "202094",  },
                };

                students.ForEach(s => context.Students.Add(s));
                await context.SaveChangesAsync();

                var subjectStudents = new List<SubjectStudents>()
                {
                    new SubjectStudents(){ SubjectId = 1, StudentId = 1},
                    new SubjectStudents(){ SubjectId = 1, StudentId = 2},
                    new SubjectStudents(){ SubjectId = 1, StudentId = 3},
                    new SubjectStudents(){ SubjectId = 1, StudentId = 4},
                    new SubjectStudents(){ SubjectId = 1, StudentId = 5},
                    new SubjectStudents(){ SubjectId = 1, StudentId = 6},
                    new SubjectStudents(){ SubjectId = 2, StudentId = 1},
                    new SubjectStudents(){ SubjectId = 2, StudentId = 2},
                    new SubjectStudents(){ SubjectId = 2, StudentId = 3},
                    new SubjectStudents(){ SubjectId = 3, StudentId = 4},
                    new SubjectStudents(){ SubjectId = 3, StudentId = 5},
                    new SubjectStudents(){ SubjectId = 3, StudentId = 6},
                    new SubjectStudents(){ SubjectId = 4, StudentId = 1},
                    new SubjectStudents(){ SubjectId = 4, StudentId = 2},
                };

                subjectStudents.ForEach(s => context.SubjectStudents.Add(s));
                await context.SaveChangesAsync();

                var grades = new List<Grade>()
                {
                    new Grade(){ SubjectID = 1, StudentID = 1, Score = 70.0},
                    new Grade(){ SubjectID = 1, StudentID = 2, Score = 80.0},
                    new Grade(){ SubjectID = 1, StudentID = 3, Score = 90.0},
                    new Grade(){ SubjectID = 1, StudentID = 4, Score = 75.0},
                    new Grade(){ SubjectID = 1, StudentID = 5, Score = 65.0},
                    new Grade(){ SubjectID = 1, StudentID = 6, Score = 50.0},
                    new Grade(){ SubjectID = 2, StudentID = 1, Score = 90.0},
                    new Grade(){ SubjectID = 2, StudentID = 2, Score = 70.0},
                    new Grade(){ SubjectID = 2, StudentID = 3, Score = 60.0},
                    new Grade(){ SubjectID = 3, StudentID = 4, Score = 80.0},
                    new Grade(){ SubjectID = 3, StudentID = 5, Score = 60.0},
                    new Grade(){ SubjectID = 3, StudentID = 6, Score = 90.0},
                    new Grade(){ SubjectID = 4, StudentID = 1, Score = 75.0},
                    new Grade(){ SubjectID = 4, StudentID = 2, Score = 85.0},
                };

                grades.ForEach(s => context.Grades.Add(s));
                await context.SaveChangesAsync();
            }
        }
    }
}
