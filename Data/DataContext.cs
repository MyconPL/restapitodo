using Microsoft.EntityFrameworkCore;
using restapitodo.Models;

namespace restapitodo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
