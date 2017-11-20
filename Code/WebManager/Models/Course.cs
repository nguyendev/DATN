using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Models
{
    public class Course : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseID { get; set; }
        public string Name { get; set; }
        public DateTime ExamTime{get;set;}
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
