using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    [Table("subject")]
    public class SubjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long subject_code { get; set; }

        [Required]
        public string subject_name { get; set; }

        public List<StudentModel> Students { get; } = new();
    }
}
