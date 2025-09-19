using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Tasks;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public TaskItem TaskItem { get; set; } = default!;

    public SelectList Users { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        Users = new SelectList(users, "Id", "Email");
        
        // Set default values
        TaskItem = new TaskItem
        {
            DueDate = DateTime.Today.AddDays(1),
            Priority = TaskPriority.Medium,
            Status = Models.TaskStatus.Pending
        };
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var users = await _userManager.Users.ToListAsync();
            Users = new SelectList(users, "Id", "Email");
            return Page();
        }

        var currentUserId = _userManager.GetUserId(User);
        TaskItem.CreatedByUserId = currentUserId;
        TaskItem.CreatedAt = DateTime.UtcNow;

        _context.Tasks.Add(TaskItem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}