using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseReg.Models
{
    public class StdLogin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string StdName { get; set; }
        public string FatherName { get; set; }
        public string Reg { get; set; }
        public string Address { get; set; }
        public string ColgJoin { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
    }
}