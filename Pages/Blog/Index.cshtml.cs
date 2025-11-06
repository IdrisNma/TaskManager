using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Blog;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<BlogPost> BlogPosts { get; set; } = default!;

    public async Task OnGetAsync()
    {
        BlogPosts = await _context.BlogPosts
            .Include(b => b.Author)
            .Where(b => b.IsPublished)
            .OrderByDescending(b => b.PublishedDate)
            .ToListAsync();
    }
}
