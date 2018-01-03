using DataAccess;
using Extension;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Areas.WebManager.ViewModels.MemberManagerViewModels;

namespace WebManager.Areas.WebManager.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(string id)
        {
            var songDbContext = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (songDbContext.IsDeleted)
                _context.Users.Remove(songDbContext);
            else
            {
                songDbContext.IsDeleted = true;
                _context.Users.Update(songDbContext);
            }
            await Save();
        }
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Member> Details(string id)
        {
            var song = await _context.Users
               .SingleOrDefaultAsync(m => m.Id == id);
            return song;
        }

        public bool Exists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<Member> Get(string id)
        {
            return await _context.Users.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EditMemberViewModels> GetEdit(string ID)
        {
            var memberDbContext = await _context.Users
                .SingleOrDefaultAsync(p => p.Id == ID);
            if (memberDbContext != null)
            {
                EditMemberViewModels editModel = new EditMemberViewModels
                {
                    ID = memberDbContext.Id,
                    Code = memberDbContext.Code,
                    Email = memberDbContext.Email,
                    UserName = memberDbContext.UserName,
                    Enrollments = memberDbContext.Enrollments,
                    HistoryLogins = memberDbContext.HistoryLogins,
                };
                return editModel;
            }
            return null;
        }

        public async Task<ListMemberManagerViewModel> GetListMember(string sortOrder, string searchString,
    int? page, int? pageSize)
        {
            var applicationDbContext = from s in _context.Users
                                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(s => s.UserName.Contains(searchString));
            } 
            switch (sortOrder)
            {
                case "code":
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
            foreach (var item in pageList)
            {
                var contact = await _context.Contact.SingleOrDefaultAsync(p => p.OwnerID == item.Id);
                string approved = contact != null ? approved = "Đã xác nhận" : approved = "Chưa xác nhận";
                SimpleMemberManagerViewModel simpleMember = new SimpleMemberManagerViewModel
                {
                    Code = item.Code,
                    CreateDT = item.CreateDT,
                    Enrollments = item.Enrollments,
                    HistoryLogins = item.HistoryLogins,
                    IsDeleted = item.IsDeleted,
                    Slug = item.Slug,
                    ID = item.Id,
                    UserName = item.UserName,
                    Approved = approved,
                };
                simpleMember.DisplayDT = item.CreateDT.HasValue ? simpleMember.DisplayDT = DateTimeExtension.CurrentDay(item.CreateDT.Value) : simpleMember.DisplayDT = "Chưa rõ";
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

        public async Task Update(EditMemberViewModels model)
        {
            var memberDbContext = await _context.Users.SingleOrDefaultAsync(p => p.Id == model.ID);

            memberDbContext.Code = model.Code;
            memberDbContext.UserName = model.UserName;
            memberDbContext.Email = model.Email;
            memberDbContext.NormalizedEmail = model.Email.ToUpper();
            memberDbContext.NormalizedUserName = model.UserName.ToUpper();
            memberDbContext.EmailConfirmed = true;
            _context.Users.Update(memberDbContext);
            await Save();
        }
    }
}
