using System;

namespace TodoDAL.Models
{
    public class Todo
    {
        public Todo(){}

        public int TodoId { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        public int StatusId { get; set; }
        public Guid UserId { get; set; }

        public Status Status { get; set; }
        public User User { get; set; }
    }
}
