using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebAppEF.Data;
using TodoWebAppEF.Models;

namespace TodoWebAppEF.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(TodoContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User) ?? string.Empty;
            var todos = await _context.Todos
                .Where(t => t.UserId == userId)
                .ToListAsync();
            return View(todos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                item.UserId = _userManager.GetUserId(User) ?? string.Empty;
                _context.Todos.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Complete(int id)
        {
            var userId = _userManager.GetUserId(User) ?? string.Empty;
            var item = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (item != null)
            {
                item.IsDone = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User) ?? string.Empty;
            var item = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (item != null)
            {
                _context.Todos.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
