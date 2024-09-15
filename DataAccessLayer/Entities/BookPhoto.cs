using DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entiies
{
    public class BookPhoto
    {
        [Key]
        public int BookPhotoId { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        public int Caption { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}