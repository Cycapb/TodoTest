using System.Collections.Generic;

namespace TodoDAL.Models
{
    public class Status
    {
        public Status() { }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public ICollection<Todo> Todos { get; set; }
    }
}