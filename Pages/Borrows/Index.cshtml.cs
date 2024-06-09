using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Pages.Borrows
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;
        
        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IList<Borrow> Borrows { get; set; }

        public async Task OnGetAsync()
        {
            Borrows = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }
    }
}
