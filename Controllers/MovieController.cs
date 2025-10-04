using Cinema_Scope.Models;
using Cinema_Scope.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

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
            ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalid");
                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                return View(movie);
            }

            var File = Request.Form.Files;
            if (!File.Any())
            {
                Console.WriteLine("ModelState invalid");
                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Select Movie Poster ");
                return View(movie);
            }
            var Poster = File.FirstOrDefault();
            var AllowedExtenshions = new List<string> { ".png", ".jpg" };
            if (!AllowedExtenshions.Contains(Path.GetExtension(Poster.FileName).ToLower()))
            {


                Console.WriteLine("ModelState invalid");
                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Only .png or .jpg image Only");
                return View(movie);

            }

            using var dataStrem = new MemoryStream();
            await Poster.CopyToAsync(dataStrem);
            var movie1 = new Movie
            { 
                Poster = dataStrem.ToArray(),
                GenreId = movie.GenreId,
                Titel = movie.Titel,
                StoryLine = movie.StoryLine,
                Year = movie.Year,
                Rate = movie.Rate,


              

            };
            context.Movies.Add(movie1);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
