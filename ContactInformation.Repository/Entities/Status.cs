using System.Collections.Generic;

namespace ContactInformation.Repository.Entities
{
    public class Status
    {
        public Status()
        {
            Contacts = new HashSet<Contact>();
        }

        public int StatusId { get; set; }   
        public string Name { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; } 
    }
}
