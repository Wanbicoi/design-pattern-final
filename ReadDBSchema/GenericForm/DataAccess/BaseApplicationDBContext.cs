using Microsoft.EntityFrameworkCore;


namespace GenericForm.DBContext
{
    public interface IApplicationDbContext<T> where T: class
    {
        DbSet<T> Set();

        public void SaveChanges();
    }
    public class BaseApplicationDbContext<T> : DbContext where T : class
    {
        private readonly string _databaseType;
        private readonly string _connectionString;

        // Constructor to accept database configuration dynamically
        public BaseApplicationDbContext(string databaseType, string connectionString)
        {
            _databaseType = databaseType;
            _connectionString = connectionString;

            Database.EnsureCreated();
        }

        // Configure the database based on type
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_databaseType) || string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("Database type or connection string is not provided.");
            }

            switch (_databaseType)
            {
                case "MySQL":
                    optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
                    break;

                case "PostgreSQL":
                    optionsBuilder.UseNpgsql(_connectionString);
                    break;

                default:
                    throw new NotSupportedException($"Database type '{_databaseType}' is not supported.");
            }
        }

        // Dynamically register the DbSet<T> at runtime
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityType = typeof(T);

            // Check if T implements IHasTableName
            if (typeof(IBaseModel).IsAssignableFrom(entityType))
            {
                // Create an instance of T to access the TableName property
                var instance = Activator.CreateInstance(entityType) as IBaseModel;

                if (instance != null)
                {
                    modelBuilder.Entity<T>();
                }
            }
            else
            {
                // Fallback to default pluralized table name
                modelBuilder.Entity<T>();
            }
        }

        // Method to load all entities of type T
        public void LoadEntities()
        {
            Set<T>().Load();
        }

        public DbSet<T> Set()
        {
            return Set<T>();
        }

        //void IApplicationDbContext<T>.SaveChanges()
        //{
        //    base.SaveChanges();
        //}
    }

    // Example usage with Product model
    public class ProductDbContext : BaseApplicationDbContext<Product>
    {
        public ProductDbContext(string databaseType, string connectionString)
            : base(databaseType, connectionString) { }
    }


    //public class ClientsDbContext : BaseApplicationDbContext<clients>
    //{
    //    public ClientsDbContext(string databaseType, string connectionString)
    //        : base(databaseType, connectionString) { }
    //}
}
