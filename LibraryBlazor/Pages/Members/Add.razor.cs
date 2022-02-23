using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages.Members
{
    public partial class Add : ComponentBase
    {
        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        private Member newMember;
        private MudForm addForm;
        private bool success;

        protected override void OnInitialized()
        {
            newMember = new Member();
        }

        private async Task OnSubmit()
        {
            await addForm.Validate();

            if (!addForm.IsValid)
            {
                return;
            }

            var response = await client.PostAsJsonAsync("members", newMember);
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
