using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Pages.Borrows
{
    public class ReturnModel : PageModel
    {
        private readonly LibraryContext _context;

        public ReturnModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrow Borrow { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Borrow = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Borrow == null)
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


            var borrowToUpdate = await _context.Borrows.FindAsync(Borrow.Id);

            if (borrowToUpdate == null)
            {
                return NotFound();
            }


            borrowToUpdate.ReturnDate = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Book returned successfully!";

            return RedirectToPage("./Index");
        }
    }
}
