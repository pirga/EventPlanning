using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class MyEventViewModel
    {
        public EventViewModel MyEvent { get; set; }
        public IEnumerable<MyEventProperty> MyEventsProperties { get; set; }
        public List<string> Key { get; set; }
        public List<string> Value { get; set; }
    }
}