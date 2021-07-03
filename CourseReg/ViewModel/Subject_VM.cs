using CourseReg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseReg.ViewModel
{
    public class Subject_VM
    {
        public List<Chemester> ScmesterList { get; set; }
        public List<Books> BookList { get; set; }
        public Books Books { get; set; }
        public CourseRegistartion CourseRegistartion { get; set; }
    
    }
    
}