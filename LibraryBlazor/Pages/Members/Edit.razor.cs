using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages.Members
{
    public partial class Edit : ComponentBase
    {
        [Parameter]
        public int id { get; set; }

        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        private Member existingMember;
        private MudForm addForm;
        private bool success;

        protected override async Task OnInitializedAsync()
        {
            existingMember = await client.GetFromJsonAsync<Member>($"members/{id}");
        }

        private async Task OnSubmit()
        {
            await addForm.Validate();

            if (!addForm.IsValid)
            {
                return;
            }

            var response = await client.PostAsJsonAsync("members", existingMember);
            var saved = await response.Content.ReadFromJsonAsync<bool>();

            if (saved)
            {
                navManager.NavigateTo("members");
            }
        }

        private void OnCancel()
        {
            navManager.NavigateTo("members");
        }
    }
}
