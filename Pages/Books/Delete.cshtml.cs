using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Threading.Tasks;

namespace LibraryManagement.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly LibraryContext _context;

        public DeleteModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book awa = await _context.Books.FindAsync(id);

            if (Book != null)
            {
                _context.Books.Remove(awa);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Book deleted successfully!";
            }





            return RedirectToPage("./Index");
        }

    }
}
