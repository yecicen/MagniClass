

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniClass.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public double Score { get; set; }

        public int StudentID { get; set; }

        public int SubjectID { get; set; }
    }
}
