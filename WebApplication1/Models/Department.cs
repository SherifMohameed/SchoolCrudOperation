using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dept_id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string dept_name { get; set; }

        public virtual List<Student> students { get; set; }
        public virtual List<DeptCrs> DeptCrs { get; set; }

    }
}