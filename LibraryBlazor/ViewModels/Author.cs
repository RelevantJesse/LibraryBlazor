using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.ViewModels;

public class Author
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }
}
