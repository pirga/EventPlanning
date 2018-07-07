using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace EventPlanning.Models
{
    public class MyEvent
    {
        public MyEvent()
        {
            MaxCountSubscribers = 1;
            DateEvent = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime DateEvent { get; set; }
        
        public int MaxCountSubscribers { get; set; }
    }
}