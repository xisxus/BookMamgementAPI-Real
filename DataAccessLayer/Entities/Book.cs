using DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entiies
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }


        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public ICollection<BookPhoto>? BookPhotos { get; set; } 


        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }


        public string Description { get; set; }
    }
}