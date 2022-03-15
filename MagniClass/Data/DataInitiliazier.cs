using MagniClass.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MagniClass.Data
{
    public static class DataInitiliazier 
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var students = new List<Student>
            {
                new Student(){ Name = "Yunus", Birthday = System.DateTime.Now },
                new Student(){ Name = "Emre", Birthday = System.DateTime.Now },
                new Student(){ Name = "Cicen", Birthday = System.DateTime.Now },
            };
                students.ForEach(s => context.Students.Add(s));

                context.SaveChanges();
            }
        }
    }
}
