using Library.Common;
using LibraryBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace LibraryBlazor.Pages
{
    public partial class CheckOutABook : ComponentBase
    {
        [Inject]
        NavigationManager navManager { get; set; }

        [Inject]
        HttpClient client { get; set; }

        private CheckedOut checkOut;
        private IEnumerable<Book> books;
        private IEnumerable<Member> members;
        private MudForm checkOutForm;
        private bool success;

        protected override async Task OnInitializedAsync()
        {
            checkOut = new CheckedOut();
            checkOut.Member = new Member();
            checkOut.Book = new Book();
            books = (await client.GetFromJsonAsync<IEnumerable<Book>>("books")).Where(b => b.CheckedOutTo == null).ToList();
            members = (await client.GetFromJsonAsync<IEnumerable<Member>>("members")).ToList();
        }

        private async Task OnSubmit()
        {
            await checkOutForm.Validate();

            if (!checkOutForm.IsValid)
            {
                return;
            }

            var response = await client.PostAsJsonAsync("CheckOut", checkOut);
            var saved = await response.Content.ReadFromJsonAsync<bool>();

            if (saved)
            {
                navManager.NavigateTo("checkedoutbooks");
            }
        }

        private void OnCancel()
        {
            navManager.NavigateTo("/");
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
