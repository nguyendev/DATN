using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Areas.WebManager.ViewModels.CommonManagerViewModels;

namespace WebManager.Areas.WebManager.ViewModels.MemberManagerViewModels
{
    public class ListMemberManagerViewModel : PageListItemTemplate
    {
        public List<SimpleMemberManagerViewModel> ListMember { get; set; }
    }

    public class SimpleMemberManagerViewModel : BaseIndexViewModel
    {
        public string ID { get; set; }
        [Display(Name ="CMND")]
        public string UserName { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<HistoryLogin> HistoryLogins { get; set; }
        public bool IsDeleted { get; set; }
        public string Slug { get; set; }
        public string DisplayDT { get; set; }
        public string Code { get; set; }
    }
}
