using System;
using System.Collections.Generic;

namespace TodoDAL.Models
{
    public class User
    {
        public User() { }

        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}