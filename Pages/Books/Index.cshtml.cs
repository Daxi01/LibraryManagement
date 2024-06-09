using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IList<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _context.Books.ToListAsync();
        }
    }
}
