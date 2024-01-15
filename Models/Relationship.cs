using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Hosting;

namespace WebApplication1.Models
{
    public class Relationship
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public StudentModel Student { get; set; } = null!;
        public SubjectModel Subject { get; set; } = null!;

    }
}