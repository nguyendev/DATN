using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Areas.MemberManager.ViewModels.MemberManagerViewModels;

namespace WebManager.Areas.MemberManager.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ListMemberManagerViewModel> GetListMember(string sortOrder, string searchString,
    int? page, int? pageSize)
        {
            var applicationDbContext = from s in _context.Users
                                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.OrderByDescending(s => s.CreateDT);
            }
            switch (sortOrder)
            {
                case "Code":
                    applicationDbContext = applicationDbContext.OrderBy(s => s.Code);
                    break;
                case "createDT":
                    applicationDbContext = applicationDbContext.OrderByDescending(s => s.CreateDT);
                    break;
                default:
                    applicationDbContext = applicationDbContext.OrderByDescending(s => s.CreateDT);
                    break;
            }
            var pageList = await PaginatedList<Member>.CreateAsync(applicationDbContext.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10);
            List<SimpleMemberManagerViewModel> listSimpleMember = new List<SimpleMemberManagerViewModel>();
            foreach (var item in applicationDbContext)
            {
                var contact = await _context.Contact.SingleOrDefaultAsync(p => p.OwnerID == item.Id);
                string approved = _context != null ? approved = "Đã xác nhận" : approved = "Chưa xác nhận";
                SimpleMemberManagerViewModel simpleMember = new SimpleMemberManagerViewModel
                {
                    Code = item.Code,
                    CreateDT = item.CreateDT,
                    Enrollments = item.Enrollments,
                    HistoryLogins = item.HistoryLogins,
                    IsDeleted = item.IsDeleted,
                    Slug = item.Slug,
                    ID = item.Id,
                    Approved = approved,
                };
                listSimpleMember.Add(simpleMember);
            }
            ListMemberManagerViewModel listMemberManagerViewModel = new ListMemberManagerViewModel
            {
                PageSize = pageList.PageSize,
                Areas = "MemberManager",
                Action = "Index",
                Controller = "MembersManager",
                Count = pageList.Count,
                TotalPages = pageList.TotalPages,
                PageIndex = pageList.PageIndex,
                ListMember = listSimpleMember

            };
            return listMemberManagerViewModel;
        }
    }
}
