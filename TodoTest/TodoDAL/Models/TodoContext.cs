using System.Data.Entity;

namespace TodoDAL.Models
{
    public class TodoContext:DbContext
    {
        public TodoContext()  : base("name=todoConnection") {}

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}