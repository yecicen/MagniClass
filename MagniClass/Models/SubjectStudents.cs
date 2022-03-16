using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class SubjectStudents
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

    }
}
