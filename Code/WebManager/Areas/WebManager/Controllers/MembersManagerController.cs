using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Extension;
using WebManager.Areas.WebManager.Data;
using WebManager.Areas.WebManager.ViewModels.MemberManagerViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebManager.Areas.MemberManager.Controllers
{
    [Area("WebManager")]
    [Authorize]
    public class MembersManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemberRepository _repository;

        public MembersManagerController(
            ApplicationDbContext context,
            IMemberRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        [Route("quan-ly/thanh-vien")]
        // GET: MemberManager/Members
        public async Task<IActionResult> Index(string sortOrder,
 string currentFilter,
    string searchString,
    int? page, int? pageSize)
        {
            List<NumberItem> SoLuong = new List<NumberItem>
            {
                new NumberItem { Value = 10},
                new NumberItem { Value = 20},
                new NumberItem { Value = 50},
                new NumberItem { Value = 100},
            };
            ViewData["SoLuong"] = SoLuong;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CodeParm"] = String.IsNullOrEmpty(sortOrder) ? "code" : "";
            ViewData["CreateDTParm"] = String.IsNullOrEmpty(sortOrder) ? "createDT" : "";
            ViewData["ViewsParm"] = String.IsNullOrEmpty(sortOrder) ? "views" : "";
            ViewData["CurrentSize"] = pageSize;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var applicationDbContext = await _repository.GetListMember(sortOrder, searchString, page, pageSize);
            return View(applicationDbContext);
        }

        // GET: MemberManager/Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: MemberManager/Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberManager/Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsDeleted,Slug,Code,CreateDT,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: MemberManager/Members/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repository.GetEdit(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberManager/Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditMemberViewModels member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(member);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: MemberManager/Members/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repository.Get(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: MemberManager/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool MemberExists(string id)
        {
            return _repository.Exists(id);
        }
    }
}
