using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using WebManager.Models.ContactViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebManager.Controllers
{
    [Route("api/ContactAPI")]
    public class ContactAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [HttpPost()]
        public async Task<bool> CreateContactAsync(CreateContactViewModel model)
        {
            try
            {
                Contact contact = new Contact { OwnerID = model.UserID };
                _context.Add(contact);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
