using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolOfFineArtsModels;

namespace SchoolOfFineArtsDB
{
    public class SchoolOfFineArtsDBContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public SchoolOfFineArtsDBContext() { }

        public SchoolOfFineArtsDBContext(DbContextOptions options)
            : base(options)
        {

        }

        //add to allow migrations when the context is not built
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                var config = builder.Build();
                var cnstr = config["ConnectionStrings:SchoolOfFineArtsDB"];
                var options = new DbContextOptionsBuilder<SchoolOfFineArtsDBContext>().UseSqlServer(cnstr);
                optionsBuilder.UseSqlServer(cnstr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>(x =>
            {
                x.HasData(
                    new Teacher() { Id = 1, FirstName = "Anne", LastName = "Sullivan", Age = 27 },
                    new Teacher() { Id = 2, FirstName = "Maria", LastName = "Montessori", Age = 32 },
                    new Teacher() { Id = 3, FirstName = "William", LastName = "McGuffey", Age = 21 },
                    new Teacher() { Id = 4, FirstName = "Emma", LastName = "Willard", Age = 47 },
                    new Teacher() { Id = 5, FirstName = "Jaime", LastName = "Escalante", Age = 62 }
                );  
            });
        }

    }
}