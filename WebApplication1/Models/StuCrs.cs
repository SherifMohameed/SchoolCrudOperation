using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class StuCrs
    {
        [ForeignKey("Student")]
        [Key]
        [Column(Order = 0)]
        public int st_id { get; set; }
        [ForeignKey("Course")]
        [Key]
        [Column(Order = 1)]
        public int crs_id { get; set; }
        public int garde { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}