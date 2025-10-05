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
            return View("MovieForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel movie)
        {
            if (!ModelState.IsValid)
            {

                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                return View("MovieForm", movie);
            }

            var File = Request.Form.Files;
            if (!File.Any())
            {

                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Select Movie Poster ");
                return View("MovieForm", movie);
            }
            var Poster = File.FirstOrDefault();
            var AllowedExtenshions = new List<string> { ".png", ".jpg" };
            if (!AllowedExtenshions.Contains(Path.GetExtension(Poster.FileName).ToLower()))
            {



                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", ".png or .jpg image Only");
                return View("MovieForm", movie);

            }

            if (Poster.Length > 1048576)
            {

                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Poster Size must be less than 1MB");
                return View("MovieForm", movie);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();

            var movieold = new MovieFormViewModel
            {
                Id = movie.id,
                Poster = movie.Poster,
                Titel = movie.Titel,
                Rate = movie.Rate,
                GenreId = movie.GenreId,
                StoryLine = movie.StoryLine,
                Year = movie.Year,

            };
            return View("MovieForm", movieold);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieFormViewModel Newmovie)
        {
            var movieId = context.Movies.Find(id);
            if (!ModelState.IsValid)
            {
                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                return View(Newmovie);

            }
            var File = Request.Form.Files;
            if (!File.Any())
            {

                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Select Movie Poster ");
                return View(Newmovie);
            }
            var Poster = File.FirstOrDefault();
            var AllowedExtenshions = new List<string> { ".png", ".jpg" };
            if (!AllowedExtenshions.Contains(Path.GetExtension(Poster.FileName).ToLower()))
            {



                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", ".png or .jpg image Only");
                return View(Newmovie);

            }

            if (Poster.Length > 1048576)
            {

                ViewData["Genre"] = await context.Genres.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Poster Size must be less than 1MB");
                return View(Newmovie);
            }
            using var dataStrem = new MemoryStream();
            await Poster.CopyToAsync(dataStrem);
            var movie1 = new Movie
            {
                Poster = dataStrem.ToArray(),
                GenreId = Newmovie.GenreId,
                Titel = Newmovie.Titel,
                StoryLine = Newmovie.StoryLine,
                Year = Newmovie.Year,
                Rate = Newmovie.Rate,

            };
            context.Movies.Add(movie1);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
