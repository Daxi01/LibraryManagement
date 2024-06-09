using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Borrows se mi nijak nepoda�ilo zprovoznit a nem��u p�ij�t na to pro�, na tvrdo z�znam p�idat jde, ale nic s n�m nejde d�lat



namespace LibraryManagement.Pages.Borrows
{
    public class CreateModel : PageModel
    {
        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrow Borrow { get; set; }

        public SelectList BookList { get; set; }
        public SelectList UserList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BookList = new SelectList(await _context.Books.ToListAsync(), "Id", "Title");
            UserList = new SelectList(await _context.Users.ToListAsync(), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            System.Console.WriteLine($"Received BookId: {Borrow.BookId}, UserId: {Borrow.UserId}");

            if (!ModelState.IsValid)
            {
                System.Console.WriteLine("Model state is not valid.");
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        System.Console.WriteLine(error.ErrorMessage);
                    }
                }

                BookList = new SelectList(await _context.Books.ToListAsync(), "Id", "Title");
                UserList = new SelectList(await _context.Users.ToListAsync(), "Id", "Name");
                return Page();
            }

            System.Console.WriteLine($"Adding Borrow record: BookId={Borrow.BookId}, UserId={Borrow.UserId}, BorrowDate={Borrow.BorrowDate}");

            Borrow.BorrowDate = DateTime.Now;
            Borrow.ReturnDate = null;

            try
            {
                _context.Borrows.Add(Borrow);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Book borrowed successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                System.Console.WriteLine($"Exception: {ex.Message}");
                BookList = new SelectList(await _context.Books.ToListAsync(), "Id", "Title");
                UserList = new SelectList(await _context.Users.ToListAsync(), "Id", "Name");
                return Page();
            }
        }
    }
}
