using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseReg.Models
{
    public class CourseRegistartion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string StdName { get; set; }
        public string Chemester { get; set; }
        public string Subjects { get; set; }
    }
}