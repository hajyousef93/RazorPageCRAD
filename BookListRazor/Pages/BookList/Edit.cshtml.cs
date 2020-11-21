using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Data;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [BindProperty]
        public Book  book { get; set; }
        public async Task OnGet(int id)
        {
            book = await _dbContext.books.FindAsync(id);
        }
        public async Task<IActionResult>OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookFromDb = await _dbContext.books.FindAsync(book.Id);
                bookFromDb.Name = book.Name;
                bookFromDb.Author = book.Author;
                bookFromDb.ISBN = book.ISBN;
                await _dbContext.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            return RedirectToPage();
            

        }
    }
}