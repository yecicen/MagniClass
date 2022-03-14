using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class Teacher: ApplicationUser
    {
        public double Salary { get; set; }
    }

}

