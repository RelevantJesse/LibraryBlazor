using Library.Common;
using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages.Authors
{
    public partial class Add : ComponentBase
    {
        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        private Author newAuthor;
        private MudForm addForm;
        private bool success;

        protected override void OnInitialized()
        {
            newAuthor = new Author();
        }

        private async Task OnSubmit()
        {
            await addForm.Validate();

            if (!addForm.IsValid)
            {
                return;
            }

            var response = await client.PostAsJsonAsync("authors", newAuthor);
            var saved = await response.Content.ReadFromJsonAsync<bool>();

            if (saved)
            {
                navManager.NavigateTo("authors");
            }
        }

        private void OnCancel()
        {
            navManager.NavigateTo("authors");
        }
    }
}
