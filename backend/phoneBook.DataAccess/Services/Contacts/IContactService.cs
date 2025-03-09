using phoneBook.Entities.DTO.Contact;
using phoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.DataAccess.Services.Contacts
{
    public interface IContactService
    {
        Task<(List<AddUpdateContactDTO>, int)> GetAllContacts(int pageNumber, int pageSize);
        Task<AddUpdateContactDTO> GetContactById(int id);
        Task<Contact> GetByName(string name);
        Task<Contact> AddContact(AddUpdateContactDTO newContact);
        Task<AddUpdateContactDTO> UpdateContact(int id, AddUpdateContactDTO updatedContact);
        Task<bool> DeleteContact(int id);
    }
}
