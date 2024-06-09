using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IList<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
