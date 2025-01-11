using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;

public static class DbContextHelper
{
    private static ApplicationDbContext? _context;

    public static ApplicationDbContext Context
    {
        get
        {
            if (_context == null)
            {
                _context = new ApplicationDbContext();
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
