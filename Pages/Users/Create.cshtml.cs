using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Threading.Tasks;

namespace LibraryManagement.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            TempData["Message"] = "User created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
