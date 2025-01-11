using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;

public static class DbContextHelper
{
    private static ClientsDbContext? _context;

    public static string? _databaseType;

    public static string? _connectionString;

    public static void SetUpDatabse(string databaseType, string connectionString)
    {
        _databaseType = databaseType;
        _connectionString = connectionString;
    }

    public static ClientsDbContext Context
    {
        get
        {
            if (_context == null)
            {
                //_context = new ApplicationDbContext(_databaseType, _connectionString);
                _context = new ClientsDbContext(_databaseType, _connectionString);
            }
            return _context;
        }
    }

    public static void DisposeContext()
    {
        _context?.Dispose();
        _context = null;
    }

    public static DbSet<T> GetDbSet<T>() where T : class
    {
        return Context.Set<T>();
    }
}
