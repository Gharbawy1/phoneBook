using AutoMapper;
using phoneBook.Entities.DTO.Contact;
using phoneBook.Entities.Models;

namespace Phonebook.Presentation.Profiles
{
    public class ContactProfile:Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, AddUpdateContactDTO>();
            CreateMap<AddUpdateContactDTO, Contact>();
        }
    }
}
