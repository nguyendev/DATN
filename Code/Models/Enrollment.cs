using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Enrollment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ID { get; set; }
        public string StudentID { get; set; }
        public int? RoomID { get; set; }
        public Room Room { get; set; }
        public Member Student { get; set; }
        public long CourseID { get; set; }
        public Course Course { get; set; }
    }
}
