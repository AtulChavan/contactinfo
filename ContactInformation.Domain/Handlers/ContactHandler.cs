using ContactInformation.Repository;
using System.Collections.Generic;
using ContactInformation.Domain.Helpers;
using System.Linq;
using System.Threading.Tasks;
using ContactInformation.Domain.Models;

namespace ContactInformation.Domain.Handlers
{
    public interface IContactHandler
    {
        Task<List<Contact>> GetContacts();
        Task<Contact> GetContact(int id);
        Task<Contact> AddContact(Contact contact);
        Task<Contact> UpdateContact(int id, Contact contact);
        Task<Contact> DeleteContact(int id);
        Task<bool> ContactExists(int id);
    }

    public class ContactHandler : IContactHandler
    {
        private readonly IContactRepository _contactRepository;
        public ContactHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            var repoContact = MappingHelper.ToRepo(contact);
            var response = await _contactRepository.AddContact(repoContact);

            return MappingHelper.ToDomain(response);
        }

        public async Task<Contact> DeleteContact(int id)
        {
            var response = await _contactRepository.DeleteContact(id);

            return MappingHelper.ToDomain(response);
        }

        public async Task<Contact> GetContact(int id)
        {
            var response = await _contactRepository.GetContact(id);

            return MappingHelper.ToDomain(response);
        }

        public async Task<List<Contact>> GetContacts()
        {
            var response = await _contactRepository.GetContacts();
            var contacts = response.ToList().Select(con => MappingHelper.ToDomain(con)).ToList();

            return contacts;
        }

        public async Task<Contact> UpdateContact(int id, Contact contact)
        {
            var repoContact = MappingHelper.ToRepo(contact, true);
            var response = await _contactRepository.UpdateContact(id, repoContact);

            return MappingHelper.ToDomain(response);
        }

        public async Task<bool> ContactExists(int id)
        {
            return await _contactRepository.ContactExists(id);
        }
    }
}
