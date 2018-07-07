using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class MyEventProperty
    {
        public MyEventProperty()
        {
            Id = 0;
            IdEvent = 0;
            IdElementOnForm = "";//Guid.NewGuid().ToString();
        }
        public MyEventProperty(string id)
        {
            Id = 0;
            IdEvent = 0;
            IdElementOnForm = id;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("MyEvent")]
        public int IdEvent { get; set; }
        public string IdElementOnForm { get; set; }
        public virtual MyEvent MyEvent { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}