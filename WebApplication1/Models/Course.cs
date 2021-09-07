using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public int crs_id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string crs_name { get; set; }

        public virtual List<StuCrs> StuCrs { get; set; }
        public virtual List<DeptCrs> DeptCrs { get; set; }

    }
}