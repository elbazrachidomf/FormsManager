using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forms.Models
{
    public class FormTitleViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Duration  { get; set; }
        public string Desc { get; set; }
        public int IDGroup { get; set; }
    }
}