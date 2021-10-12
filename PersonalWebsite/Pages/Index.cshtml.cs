using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<Post> Posts { get; set; }

        public async Task OnGetAsync()
        {
            
           
            Posts = await db.Posts
                .OrderByDescending(m => m.PostId)
                .Take(4)
                .Include(c => c.Category)
                .AsNoTracking()
                .ToListAsync();

        }


    }
}
