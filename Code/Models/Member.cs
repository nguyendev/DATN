using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Member : IdentityUser
    {
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<HistoryLogin> HistoryLogins { get; set; }
        public bool IsDeleted { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
  
        public DateTime? CreateDT { get; set; }
    }
}
