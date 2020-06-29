using ContactInformation.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactInformation.Repository.Context
{
    public class ContactDbContext : DbContext
    {
        private const string CONNECTION_STRING_KEY = "ContactCS";
        private readonly string _connectionString;
        private const string DB_USER_NAME = "contactadmin";
        private const string DB_USER_PASSWORD = "EWBKD3C93JfjxNX";

        public ContactDbContext()
        {
        }

        public ContactDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(CONNECTION_STRING_KEY);
            if (!string.IsNullOrEmpty(_connectionString))
                _connectionString = string.Format(_connectionString, DB_USER_NAME, DB_USER_PASSWORD);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => new { e.Id })
                    .HasName("pk_contact");

                entity.ToTable("Contact");

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired();

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("fk_contact_status");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Status>().HasData(
                new Status() { StatusId = 0, Name = "Inactive" },
                new Status() { StatusId = 1, Name = "Active" });
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
