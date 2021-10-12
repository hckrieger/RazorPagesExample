using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data;
using PersonalWebsite.Models;

namespace PersonalWebsite.Pages.Posts
{
    public class ArchivesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ArchivesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Posts { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts
                .OrderByDescending(m => m.PostId)
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
