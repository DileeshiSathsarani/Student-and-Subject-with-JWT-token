using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class SubjectDTO
    {   
        public long Id {  get; set; }
        [Required]
        public long subject_code { get; set; }

        [Required]
        public string subject_name { get; set; }
    }
}
