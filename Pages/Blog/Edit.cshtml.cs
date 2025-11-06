using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using System.Security.Claims;

namespace TaskManager.Pages.Blog;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
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

        var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(m => m.Id == id);
        
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Check if the current user is the author
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (BlogPost.AuthorId != userId)
        {
            return Forbid();
        }

        BlogPost.LastModifiedDate = DateTime.UtcNow;
        
        _context.Attach(BlogPost).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BlogPostExists(BlogPost.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Details", new { id = BlogPost.Id });
    }

    private bool BlogPostExists(int id)
    {
        return _context.BlogPosts.Any(e => e.Id == id);
    }
}
