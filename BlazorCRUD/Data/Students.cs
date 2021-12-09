using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD.Data
{
    public class Students
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int id { get; set; }
        public int class_id { get; set; }
        public int country_id { get; set; }
        public string name { get; set; }
        public DateTime? date_of_birth { get; set; } = DateTime.MinValue;
      //  public List<countries> countriesList { get; set; }
    }
}
