using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Areas.WebManager.ViewModels.MemberManagerViewModels;

namespace WebManager.Areas.WebManager.Data
{
    public interface IMemberRepository
    {
        Task<ListMemberManagerViewModel> GetListMember(string sortOrder, string searchString,
    int? page, int? pageSize);
        Task<Member> Get(string id);
        bool Exists(string id);
        Task<EditMemberViewModels> GetEdit(string ID);
        Task Update(EditMemberViewModels model);
        Task<Member> Details(string id);
        Task Delete(string id);
        // Save and Edit Publish Datetime
    }
}
