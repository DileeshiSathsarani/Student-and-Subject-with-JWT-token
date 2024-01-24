using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;

namespace WebApplication1.Models
{
    [Table("student")]
    public class StudentModel

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } // primary key 

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string user_name { get; set; }

        [Required]
        public string password { get; set; }
        [Required]
        public string contact_number { get; set; }

        public List<SubjectModel> Subjects { get; } = new();

    }

}
