using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseReg.ViewModel
{
    public class Subject_VMQ
    {
        public int id { get; set; }
        public string ChemsterName { get; set; }
        public string Name { get; set; }
    }
    public class Course_VMQ
    {
        public int id { get; set; }
        public string StdName { get; set; }
        public string ChemesterName { get; set; }
        public string BookName { get; set; }
    }
}