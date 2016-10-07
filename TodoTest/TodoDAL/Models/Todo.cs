using System;

namespace TodoDAL.Models
{
    public class Todo
    {
        public Todo(){}

        public int TodoId { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool Complete { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
