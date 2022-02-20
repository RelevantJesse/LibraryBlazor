using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.ViewModels
{
    public class Book
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(50)]
        public string ISBN { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Author Author { get; set; }

        [Required]
        public int Genre { get; set; }

        #nullable enable
        public Member? CheckedOutTo { get; set; }
        #nullable disable
    }
}
