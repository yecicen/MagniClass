using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class Student : Person
    {
        public string RegistrationNumber { get; set; }
    }
}
