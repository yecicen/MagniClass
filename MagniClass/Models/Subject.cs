using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int TeacherID { get; set; }

        
    }
}
