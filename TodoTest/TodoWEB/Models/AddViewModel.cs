using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TodoWEB.Models
{
    public class AddViewModel
    {
        [Required(ErrorMessage = "Не указано описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Дата выполнения")]
        public DateTime DtEnd { get; set; }
        public bool Complete { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
    }
}