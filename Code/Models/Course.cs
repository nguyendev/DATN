using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Course : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long CourseID { get; set; }
        public string Name { get; set; }
        public List<CourseRoom> ListRoom { get; set; }
        public DateTime ExamTime{get;set;}
        //public List<Enrollment> Enrollments { get; set; }

    }
}
