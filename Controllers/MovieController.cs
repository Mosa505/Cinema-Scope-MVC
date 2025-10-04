using Cinema_Scope.Models;
using Cinema_Scope.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Scope.Controllers
{
    public class MovieController : Controller
    {
        private readonly CinemaDbContext context;

        public MovieController(CinemaDbContext context)
        {
            this.context = context;
        }


        public async Task<IActionResult> Index()
        {
            var Movie = await context.Movies.ToListAsync();
            return View(Movie);
        }

        public async Task<IActionResult> Create()
        {
            var ViewGanera = new MovieFormViewModel
            {
                Genres = await context.Genres.OrderBy(e => e.Name).ToListAsync(),
            };
            return View(ViewGanera);
        }
    }
}
