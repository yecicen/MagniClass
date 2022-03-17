using System;
using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
