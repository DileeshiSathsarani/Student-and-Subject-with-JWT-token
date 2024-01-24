using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class LoginDTO
    {
        public string token { get; set; }

        [Required]
        public string user_name { get; set; }

        [Required]
        public string password { get; set; }
    }
}
