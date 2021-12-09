using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD.Data
{
    public class StudentModel
    {
        public StudentModel()
        {
            this.CountryList = new List<SelectListItem>();

            this.ClassesList = new List<SelectListItem>();
        }
        public int student_id { get; set; }
        public int class_id { get; set; }
        public int country_id { get; set; }
        public string student_name { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string country_name { get; set; }
        public string class_name { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }

        public IEnumerable<SelectListItem> ClassesList { get; set; }
    }
}
