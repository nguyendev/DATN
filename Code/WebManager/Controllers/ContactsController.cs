using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using WebManager.Models.ContactViewModels;
using WebManager.Repository;

namespace WebManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactRepository _repository;

        public ContactsController(ApplicationDbContext context,
            IContactRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contact.Include(c => c.Image).Include(c => c.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Image)
                .Include(c => c.Owner)
                .SingleOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        [Route("cap-nhat-thong-tin/tao-moi/{code}")]
        public IActionResult Create(string code)
        {
            if (_context.Users.Any(p => p.Code == code))
            {
                var userId = _context.Users.SingleOrDefault(p => p.Code == code).Id;
                if (_context.Contact.Any(p => p.OwnerID == userId))
                    return RedirectToAction("Edit", new { id = _context.Contact.SingleOrDefaultAsync(p => p.OwnerID == userId).Id });
                ViewData["code"] = code;
                return View(new CreateContactViewModel {Code = code });
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("cap-nhat-thong-tin/tao-moi/{code}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContactViewModel contact)
        {
            contact.DateofBirth = DateTime.Now;
            contact.DateIdentityCard = DateTime.Now;
            if (ModelState.IsValid)
            {
                if(await _repository.Create(contact))
                    return RedirectToAction("SetPassword", "Manage",new {code =  contact.Code});
                ModelState.AddModelError(string.Empty, "Không tạo được.");
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["ImageID"] = new SelectList(_context.Image, "ImageID", "ImageID", contact.ImageID);
            ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id", contact.OwnerID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ContactId,OwnerID,FirstMidName,LastName,ImageID,Sex,Landline,MobilePhone,DateIdentityCard,WhereIdentityCard,Address,Zip,DateofBirth,Status,CreateDT,UpdateDT,IsDeleted")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            ViewData["ImageID"] = new SelectList(_context.Image, "ImageID", "ImageID", contact.ImageID);
            ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id", contact.OwnerID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Image)
                .Include(c => c.Owner)
                .SingleOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.ContactId == id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContactExists(long id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
