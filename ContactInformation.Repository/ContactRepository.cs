using ContactInformation.Repository.Context;
using ContactInformation.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInformation.Repository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContact(int id);
        Task<Contact> AddContact(Contact contact);
        Task<Contact> UpdateContact(int id, Contact contact);
        Task<Contact> DeleteContact(int id);
        Task<bool> ContactExists(int id);
    }

    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _dbContext;
        public ContactRepository(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            _dbContext.Contact.Add(contact);
            await _dbContext.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> DeleteContact(int id)
        {
            var contact = await GetContact(id);

            if (contact != null)
            {
                contact.StatusId = 0;
                return await UpdateContact(id, contact);
            }
            return null;
        }

        public async Task<Contact> GetContact(int id)
        {
            var contact = await _dbContext.Contact.FindAsync(id);

            return contact;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _dbContext.Contact.ToListAsync();
        }

        public async Task<Contact> UpdateContact(int id, Contact contact)
        {
            var dbContact = await GetContact(id);

            if (dbContact != null)
            {
                dbContact.FirstName = contact.FirstName;
                dbContact.LastName = contact.LastName;
                dbContact.Email = contact.Email;
                dbContact.Phone = contact.Phone;
                dbContact.StatusId = contact.StatusId;

                _dbContext.Update(dbContact);
                await _dbContext.SaveChangesAsync();
                return dbContact;
            }
            return null;
        }

        public async Task<bool> ContactExists(int id)
        {
            return await _dbContext.Contact.AnyAsync(e => e.Id == id);
        }
    }
}
