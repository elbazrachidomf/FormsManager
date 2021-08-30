using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forms.Models
{
    public class PreparedFieldViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? PreparedGroupFieldID { get; set; }
    }
}