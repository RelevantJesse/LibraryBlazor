using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.ViewModels
{
    public class Member
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}