using Microsoft.EntityFrameworkCore;

namespace GenericForm.DBContext
{

    public interface IBaseModel
    {
        //public int ID { get; set; }
        //public string TableName { get; set; }
    }
    public class User : IBaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public int Age { get; set; }

        // Implement TableName property from IBaseModel interface
        //public string TableName { get; set; } = "Users"; // Set table name to "Users"

    }
    public class Product : IBaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        // Implement TableName property from IBaseModel interface
        //public string TableName { get; set; } = "Products"; // Set table name to Product
    }
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
            SeedData();
        }

        private void SeedData()
        {
            if (!Users.Any())
            {
                Users.AddRange(
                    new User { Name = "John Doe", Age = 30, ID = 3 },
                    new User { Name = "Jane Smith", Age = 25, ID = 2 },
                    new User { Name = "Peter Jones", Age = 40, ID = 1 }
                );
            }
            if (!Products.Any())
            {
                Products.AddRange(
                    new Product { Name = "Laptop", Age = 1200, ID = 3 },
                    new Product { Name = "Keyboard", Age = 75, ID = 2 },
                    new Product { Name = "Mouse", Age = 25, ID = 1 }
                );
            }
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={Environment.CurrentDirectory}/sqlite.db");

        public void AddDynamicModelToDbContext(string modelName, Type modelType)
        {
            var dbSetProperty = typeof(ApplicationDbContext)
                .GetProperty(modelName);

            if (dbSetProperty == null)
            {
                throw new Exception($"DbSet for {modelName} not found in ApplicationDbContext");
            }

            dbSetProperty.SetValue(this, modelType);
        }

    }
}
