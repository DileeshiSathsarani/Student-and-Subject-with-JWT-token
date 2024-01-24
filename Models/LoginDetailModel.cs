using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class LoginDetailModel
    {   
        public long Id { get; set; }

        [Required]
        public string user_name { get; set; }
        [Required]
        public string password { get; set; }
        public string token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
