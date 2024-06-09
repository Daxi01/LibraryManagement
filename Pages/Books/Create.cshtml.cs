using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Threading.Tasks;

namespace LibraryManagement.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

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

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Book created succesfully!";

            return RedirectToPage("./Index");
        }
    }
}
