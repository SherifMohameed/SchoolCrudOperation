using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DeptCrs
    {
        [ForeignKey("Department")]
        [Key]
        [Column(Order = 0)]
        public int dept_id { get; set; }
        [ForeignKey("Course")]
        [Key]
        [Column(Order = 1)]
        public int crs_id { get; set; }

        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
    }
}