using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CourseRoom
    {
        public long CourseID { get; set; }

        public Course Course { get; set; }
        public  int RoomID { get; set; }

        public Room Room { get; set; }
    }
}
