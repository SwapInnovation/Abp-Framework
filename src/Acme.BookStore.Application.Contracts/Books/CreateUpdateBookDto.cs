using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public BookType Type { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        [Required]
        public float Price { get; set; }
        public Guid AuthorId { get; set; }


    }
}
