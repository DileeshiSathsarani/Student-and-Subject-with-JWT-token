using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Requests
{
    public class CreateSubjectRequest
    {
        [Required]
        public long subject_code { get; set; }

        [Required]
        public string subject_name { get; set; }
    }
}
