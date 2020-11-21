using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Data;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable <Book> books { get; set; }
        public async Task OnGet()
        {
            books = await _dbContext.books.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _dbContext.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}