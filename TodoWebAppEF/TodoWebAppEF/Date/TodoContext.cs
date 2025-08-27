using Microsoft.EntityFrameworkCore;
using TodoWebAppEF.Models;

namespace TodoWebAppEF.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
