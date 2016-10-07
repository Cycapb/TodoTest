using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWEB.Models
{
    public class AddViewModel
    {
        [Required(ErrorMessage = "Не указано описание")]
        public string Description { get; set; }
        public DateTime DtEnd { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
    }
}