using fit_box.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fit_box.Controllers
{
    [Authorize]
    public class IngredientesController : Controller
    {
        private readonly AppDbContext _context;
        public IngredientesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ingredientes = await _context.Ingredientes
                .Where(i => i.LoginId.ToString() == userId)
                .ToListAsync();
            return View(ingredientes);
        }
    }

}
