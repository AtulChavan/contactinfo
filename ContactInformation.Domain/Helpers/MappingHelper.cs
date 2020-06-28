using R = ContactInformation.Repository.Entities;
using D = ContactInformation.Domain.Models;

namespace ContactInformation.Domain.Helpers
{
    public class MappingHelper
    {
        public static D.Contact ToDomain(R.Contact repoContact)
        {
            var contact = new D.Contact
            {
                Id = repoContact.Id,
                Email = repoContact.Email,
                Name = $"{repoContact.FirstName} {repoContact.LastName}",
                Phone = repoContact.Phone,
                Active = repoContact.StatusId == 0 ? false : true
            };

            return contact;
        }

        public static R.Contact ToRepo(D.Contact domainContact, bool isUpdate = false)
        {
            var name = domainContact.Name.Split(' ');
            var contact = new R.Contact
            {
                Email = domainContact.Email,
                FirstName = name.Length > 0 ? name[0] : "",
                LastName = name.Length > 1 ? name[1] : "",
                Phone = domainContact.Phone,
                StatusId = domainContact.Active ? 1 : 0
            };

            if (isUpdate) contact.Id = domainContact.Id;

            return contact;
        }
    }
}
