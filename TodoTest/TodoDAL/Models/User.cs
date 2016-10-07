using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoDAL.Models
{
    public class User
    {
        public User() { }

        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserName { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}