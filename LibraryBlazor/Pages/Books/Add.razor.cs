using Library.Common;
using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages.Books
{
    public partial class Add : ComponentBase
    {
        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        private Book newBook;
        private IEnumerable<Author> authors;
        private IEnumerable<Genres> genres;
        private MudForm addForm;
        private bool success;

        protected override async Task OnInitializedAsync()
        {
            newBook = new Book();
            newBook.Author = new Author();
            authors = (await client.GetFromJsonAsync<IEnumerable<Author>>("authors")).Where(a => a.IsActive).ToList();
            genres = Enum.GetValues(typeof(Genres)).Cast<Genres>().ToList();
        }

        private async Task OnSubmit()
        {
            await addForm.Validate();

            if (!addForm.IsValid)
            {
                return;
            }

            var response = await client.PostAsJsonAsync<Book>("books", newBook);
            var saved = await response.Content.ReadFromJsonAsync<bool>();

            if (saved)
            {
                navManager.NavigateTo("books");
            }
        }

        private void OnCancel()
        {
            navManager.NavigateTo("books");
        }

        private IEnumerable<string> ValidAuthor(int id)
        {
            if (id <= 0)
            {
                yield return "Select an author";
            }
        }
    }
}
