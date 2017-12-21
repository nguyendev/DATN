using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Models.ContactViewModels;

namespace WebManager.Repository
{
    public class ContactRepository : IContactRepository
    {
        private ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CreateContactViewModel model)
        {
            try
            {
                string OwnerID = (await _context.Users.SingleOrDefaultAsync(p => p.Code == model.Code)).Id;
                long? ImageID = _context.Image.Any(p => p.OwnerID == OwnerID) ? ImageID = (_context.Image.SingleOrDefault(p => p.OwnerID == OwnerID)).ImageID : ImageID = null;
                Contact contact = new Contact
                {
                    Zip = model.Zip,
                    Address = model.Address,
                    CreateDT = DateTime.Now,
                    WhereIdentityCard = model.WhereIdentityCard,
                    Status = ContactStatus.Approved,
                    DateIdentityCard = model.DateIdentityCard,
                    IsDeleted = false,
                    DateofBirth = model.DateofBirth,
                    FirstMidName = model.FirstMidName,
                    Landline = model.Landline,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    MobilePhone = model.MobilePhone,
                    OwnerID = OwnerID,
                    ImageID = ImageID
                };
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
