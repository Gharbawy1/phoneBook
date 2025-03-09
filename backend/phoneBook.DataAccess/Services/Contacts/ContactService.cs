using AutoMapper;
using phoneBook.Entities.DTO.Contact;
using phoneBook.Entities.IRepository;
using phoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.DataAccess.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }


        public async Task<Contact> AddContact(AddUpdateContactDTO newContact)
        {
            var contactEntity = _mapper.Map<Contact>(newContact);
            await _contactRepository.Add(contactEntity);
            return contactEntity; // For Response
        }

        public async Task<bool> DeleteContact(int id)
        {
            var existingContact = await _contactRepository.GetById(id);
            if (existingContact == null) return false;

            _contactRepository.Delete(existingContact);
            return true;
        }

        public async Task<(List<AddUpdateContactDTO>, int)> GetAllContacts(int pageNumber, int pageSize)
        {
            var (contacts, totalCount) = await _contactRepository.GetAllAsync(pageNumber, pageSize);

            var contactDTOs = _mapper.Map<List<AddUpdateContactDTO>>(contacts);

            return (contactDTOs, totalCount);
        }

        public async Task<Contact> GetByName(string name)
        {
            return await _contactRepository.GetByName(name);
        }

        public async Task<AddUpdateContactDTO> GetContactById(int id)
        {
            var contact = await _contactRepository.GetById(id);
            return contact != null ? _mapper.Map<AddUpdateContactDTO>(contact) : null;
        }

        public async Task<AddUpdateContactDTO> UpdateContact(int id, AddUpdateContactDTO updatedContact)
        {
            var existingContact = await _contactRepository.GetById(id);
            if (existingContact == null) return null;

            existingContact.Name = updatedContact.Name;
            existingContact.Email = updatedContact.Email;
            existingContact.PhoneNumber = updatedContact.PhoneNumber;
            _contactRepository.Update(existingContact);

            return _mapper.Map<AddUpdateContactDTO>(existingContact);
        }
    }
}
