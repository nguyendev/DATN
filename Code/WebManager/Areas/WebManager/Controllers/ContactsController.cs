using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace WebManager.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: WebManager/Contacts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contact.Include(c => c.Image).Include(c => c.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WebManager/Contacts/Details/5
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

        // GET: WebManager/Contacts/Create
        public IActionResult Create()
        {
            ViewData["ImageID"] = new SelectList(_context.Image, "ImageID", "ImageID");
            ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: WebManager/Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,OwnerID,FirstMidName,LastName,ImageID,Address,City,State,Zip,DateofBirth,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ImageID"] = new SelectList(_context.Image, "ImageID", "ImageID", contact.ImageID);
            ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id", contact.OwnerID);
            return View(contact);
        }

        // GET: WebManager/Contacts/Edit/5
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

        // POST: WebManager/Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ContactId,OwnerID,FirstMidName,LastName,ImageID,Address,City,State,Zip,DateofBirth,Status")] Contact contact)
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

        // GET: WebManager/Contacts/Delete/5
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

        // POST: WebManager/Contacts/Delete/5
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
