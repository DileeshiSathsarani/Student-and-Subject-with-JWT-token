using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApplication1.Helpers.Utils;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<LoginDetailModel> LoginDetails { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        public ApplicationDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectModel>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Subjects)
                .UsingEntity<Relationship>();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GlobalAttributes.mySqlConfiguration.connectionString, ServerVersion.AutoDetect(GlobalAttributes.mySqlConfiguration.connectionString));

            }
        }
    }
}
