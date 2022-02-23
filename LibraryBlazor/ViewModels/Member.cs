using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.ViewModels
{
    public class Member
    {
        public Member()
        {
            IsActive = true;
        }
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}