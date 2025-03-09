using Microsoft.EntityFrameworkCore;
using phoneBook.DataAccess.ApplicationContext;
using phoneBook.Entities.IRepository;
using phoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.DataAccess.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly phoneBookDbContext _dbContext;

        public ContactRepository(phoneBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<Contact>,int)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _dbContext.Contacts.CountAsync(); // إجمالي عدد الـ Contacts

            var contacts = await _dbContext.Contacts
                .Skip((pageNumber - 1) * pageSize) // تخطي الصفحات السابقة
                .Take(pageSize)                    // احضر الصفحة المطلوبة
                .ToListAsync();

            return (contacts, totalCount);
        }

        public async Task<Contact> GetById(int id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            return contact;
        }

        public async Task<Contact> GetByName(string name)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Name == name);
            return contact;
        }

        public async Task<Contact> Add(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public Contact Update(Contact contact)
        {
            _dbContext.Update(contact);
            _dbContext.SaveChanges();
            return contact;
        }

        public Contact Delete(Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();
            return contact;
        }

        

        

        
    }
}
