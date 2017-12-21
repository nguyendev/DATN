using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Models.ContactViewModels;

namespace WebManager.Repository
{
    public interface IContactRepository
    {
        Task<bool> Create(CreateContactViewModel model);
    }
}
