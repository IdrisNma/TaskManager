using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Blog;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public BlogPost? BlogPost { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BlogPost = await _context.BlogPosts
            .Include(b => b.Author)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (BlogPost == null)
        {
            return NotFound();
        }

        // Increment view count
        BlogPost.ViewCount++;
        await _context.SaveChangesAsync();

        return Page();
    }
}
