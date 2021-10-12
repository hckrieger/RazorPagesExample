using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data;
using PersonalWebsite.Models;

namespace PersonalWebsite.Pages.Posts
{
    [Authorize]
    public class EditModel : CategoryNamePageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.PostId == id);

            if (Post == null)
            {
                return NotFound();
            }
            PopulateCategoriesDropDownList(_context, Post.CategoryId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postToUpdate = await _context.Posts.FindAsync(id);

            if (postToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Post>(
                postToUpdate,
                "post",
                c => c.PostId, c => c.Title, c => c.CategoryId, c => c.DatePublished, c => c.Content))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateCategoriesDropDownList(_context, postToUpdate.CategoryId);
            return Page();
        }

       
    }
}
