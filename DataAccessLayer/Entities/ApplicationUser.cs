using DataAccessLayer.Entiies;
using Microsoft.AspNetCore.Identity;


namespace DataAccessLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<UserPhoto> UserPhotos { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
