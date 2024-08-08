using Microsoft.EntityFrameworkCore;
namespace Library_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Book> Books => Set<Book>();
    }
}
