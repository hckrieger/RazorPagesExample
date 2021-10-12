using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Posts
{
    public class CategoryNamePageModel : PageModel
    {
        public SelectList CategoryNameSL { get; set; }

        public void PopulateCategoriesDropDownList(ApplicationDbContext _context,
            object selectCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                  orderby d.Name
                                  select d;
            CategoryNameSL = new SelectList(categoriesQuery.AsNoTracking(),
                "CategoryId", "Name", selectCategory);
        }
    }
}
