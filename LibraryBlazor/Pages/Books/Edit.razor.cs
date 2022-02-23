using Library.Common;
using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages.Books
{
    public partial class Edit : ComponentBase
    {
        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        [Parameter]
        public int id { get; set; }

        private Book existingBook;
        private IEnumerable<Author> authors;
        private IEnumerable<Genres> genres;
        private MudForm addForm;

        protected override async Task OnInitializedAsync()
        {
            existingBook = await client.GetFromJsonAsync<Book>($"books/{id}");
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

            var response = await client.PostAsJsonAsync<Book>("books", existingBook);
            var success = await response.Content.ReadFromJsonAsync<bool>();

            if (success)
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
