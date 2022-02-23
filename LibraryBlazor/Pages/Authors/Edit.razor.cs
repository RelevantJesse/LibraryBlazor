using Library.Common;
using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages.Authors
{
    public partial class Edit : ComponentBase
    {
        [Parameter]
        public int id { get; set; }

        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        private Author existingAuthor;
        private MudForm addForm;
        private bool success;

        protected override async Task OnInitializedAsync()
        {
            existingAuthor = await client.GetFromJsonAsync<Author>($"authors/{id}");
        }

        private async Task OnSubmit()
        {
            await addForm.Validate();

            if (!addForm.IsValid)
            {
                return;
            }

            var response = await client.PostAsJsonAsync("authors", existingAuthor);
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
