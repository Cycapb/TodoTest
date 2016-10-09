using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TodoWEB.Models
{
    public class EditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Не задано описание")]
        public string Description { get; set; }
        [Display(Name = "Дата выполнения")]
        public DateTime DtEnd { get; set; }
    }
}