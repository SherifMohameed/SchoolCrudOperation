using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string name { get; set; }
        [Range(10, 35)]
        public int age { get; set; }
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [NotMapped]
        public string cfPassword { get; set; }
        public string image { get; set; }
        [ForeignKey("Department")]
        public int dept_id { get; set; }

        public virtual Department Department { get; set; }
        public virtual List<StuCrs> StuCrs { get; set; }
    }
}