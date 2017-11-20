using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Member : IdentityUser
    {
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public long ImageID { get; set; }
        public Image Image { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<HistoryLogin> HistoryLogins { get; set; }
    }
}
