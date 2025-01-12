using GenericForm.DBContext;
namespace GenericForm
{
internal static class Program
{
[STAThread]
static void Main()
{
ApplicationConfiguration.Initialize();
Application.Run(new MainWindow("SQL Server", "Data Source=HAICHANNGUYEN\\BI2425;Initial Catalog=winformdb;Integrated Security=True;Trust Server Certificate=True"));
}
}
}
