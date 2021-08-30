using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forms.Models
{
    public class SectionViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int? Number { get; set; }
        public int? FormId { get; set; }
        public string fTitle { get; set; }
    }
}