using System.ComponentModel.DataAnnotations.Schema;

namespace ContactInformation.Repository.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int StatusId { get; set; }   
        public virtual Status Status { get; set; }
    }
}
