using System.ComponentModel.DataAnnotations;

namespace MagniClass.Models
{
    public class CourseSubjects
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
    }
}
