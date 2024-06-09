using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Threading.Tasks;
using System.Linq;

namespace LibraryManagement.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
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

            Book = await _context.Books.FindAsync(id);

            if(Book == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Message"] = "Book updated successfully!";
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");

        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
