using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD.Data
{
    public class Statistics
    { 
        public int NoOfStudentsPerClass { get; set; }
        public int NoOfStudentsPerCountry { get; set; }
        public DateTime? AverageAgeOfStudents { get; set; }
        public string Country { get; set; }
        public string Class { get; set; } 

    }
}
