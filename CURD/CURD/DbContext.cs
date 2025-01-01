using Microsoft.EntityFrameworkCore;
namespace CURD
{
    public class User
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={Environment.CurrentDirectory}/sqlite.db");
    }

}
