using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            MaxCountSubscribers = 1;
            StateFollow = 0;
            //DateEvent = DateTime.Now;
        }
        
        public int Id { get; set; }

        public int StateFollow { get; set; }

        [Display(Name = "Название события", Prompt = "Название события")]
        [Required(ErrorMessage = "Поле Название события должно быть заполненно")]
        public string Name { get; set; }

        [Display(Name = "Дата события", Prompt = "Дата события")]
        [DataType(DataType.DateTime)]
        //[Required(ErrorMessage = "Поле Дата события должно быть заполненно")]
        public DateTime DateEvent { get; set; }

        [Display(Name = "Максимальное количество подписчиков на событие", Prompt = "Максимальное количество подписчиков на событие")]
        [Required(ErrorMessage = "Поле Максимальное количество подписчиков должно быть заполненно")]
        [Range(typeof(int), "1", "1000", ErrorMessage = "Максимальное количество подписчиков должно быть от 1 до 1000")]
        public int MaxCountSubscribers { get; set; }
    }
}