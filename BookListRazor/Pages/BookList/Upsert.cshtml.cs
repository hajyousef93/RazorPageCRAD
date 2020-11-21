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
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public UpsertModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [BindProperty]
        public Book book { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            book = new Book();
            if (id == null)
            {
                //For Create
                return Page();
            }
            //for Update
            book = await _dbContext.books.FirstOrDefaultAsync(u => u.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Page();
           
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (book.Id == 0)
                {
                      _dbContext.Add(book);
                }
                else
                {
                     _dbContext.Update(book);
                }
                await _dbContext.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            return RedirectToPage();


        }
    }
}