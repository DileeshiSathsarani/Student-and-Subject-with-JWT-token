using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class MessageDTO
    {
        private string v;

        public MessageDTO(string v)
        {
            this.v = v;
        }

        [Required]
        public string message { get; set; }
    }
}
