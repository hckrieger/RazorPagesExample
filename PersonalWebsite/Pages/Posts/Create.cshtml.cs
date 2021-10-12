using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalWebsite.Data;
using PersonalWebsite.Models;

namespace PersonalWebsite.Pages.Posts
{
    [Authorize]
    public class CreateModel : CategoryNamePageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateCategoriesDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyPost = new Post();

            if (await TryUpdateModelAsync<Post>(
                emptyPost,
                "post",
                s => s.PostId, s => s.Title, s => s.DatePublished, 
                s => s.CategoryId, s => s.Content))
            {
                _context.Posts.Add(emptyPost);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateCategoriesDropDownList(_context, emptyPost.CategoryId);
            return RedirectToPage("./Index");
        }
    }
}
