using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using System.Security.Claims;

namespace TaskManager.Pages.Blog;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public BlogPost BlogPost { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blogPost = await _context.BlogPosts
            .Include(b => b.Author)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (blogPost == null)
        {
            return NotFound();
        }

        // Check if the current user is the author
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (blogPost.AuthorId != userId)
        {
            return Forbid();
        }

        BlogPost = blogPost;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blogPost = await _context.BlogPosts.FindAsync(id);

        if (blogPost == null)
        {
            return NotFound();
        }

        // Check if the current user is the author
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (blogPost.AuthorId != userId)
        {
            return Forbid();
        }

        _context.BlogPosts.Remove(blogPost);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
