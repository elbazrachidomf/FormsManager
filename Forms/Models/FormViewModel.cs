using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forms.Models
{
    public class FormViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int? duration { get; set; }
        public int? IdGroup { get; set; }
        public string groupName { get; set; }

    }
}