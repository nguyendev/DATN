using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Areas.MemberManager.ViewModels.MemberManagerViewModels;

namespace WebManager.Areas.MemberManager.Data
{
    public interface IMemberRepository
    {
        Task<ListMemberManagerViewModel> GetListMember(string sortOrder, string searchString,
    int? page, int? pageSize);
    }
}
