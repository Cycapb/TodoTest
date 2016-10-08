using System;

namespace TodoWEB.Models
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DtEnd { get; set; }
    }
}