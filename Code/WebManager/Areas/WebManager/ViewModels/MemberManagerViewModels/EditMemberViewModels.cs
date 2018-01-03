using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Areas.WebManager.ViewModels.MemberManagerViewModels
{
    public class EditMemberViewModels
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<HistoryLogin> HistoryLogins { get; set; }
        public string Code { get; set; }

    }
}
