using phoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.Entities.IRepository
{
    public interface IContactRepository
    {
        Task<(List<Contact>, int)> GetAllAsync(int pageNumber, int pageSize);
        Task<Contact> GetById(int id);
        Task<Contact> GetByName(string name);
        Task<Contact> Add(Contact contact);
        Contact Update(Contact contact);
        Contact Delete(Contact contact);
    }
}
