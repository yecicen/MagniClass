using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class Student : ApplicationUser
    {
        public string RegistrationNumber { get; set; }
    }
}
