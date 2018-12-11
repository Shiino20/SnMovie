using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snmovie.Models;
using Snmovie.ViewModels;


namespace Snmovie.Controllers
{
  
  public class MoviesController : Controller
    {
    // GET: Movies

    private ApplicationDbContext _context;
    public MoviesController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      _context.Dispose();
    }

    public ViewResult Index()
    {
      var movies = _context.Movies.Include(m => m.Genre).ToList();

      return View(movies);
    }

    public ViewResult New()
    {
      var genres = _context.Genres.ToList();

      var viewModel = new MovieFormViewModel
      {
        Genres = genres
      };

      return View("MovieForm", viewModel);
    }

    public ActionResult Edit(int id)
    {
      var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
      var genres = _context.Genres.ToList();

      if (movie == null)
        return HttpNotFound();

      var viewModel = new MovieFormViewModel(movie)
      {
        Genres = genres
      };
      return View("MovieForm", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save(Movie movie)
    {
      if (!ModelState.IsValid)
      {
        var viewModel = new MovieFormViewModel(movie)
        {
          Genres = _context.Genres.ToList()
        };
        return View("MovieForm", viewModel);
      }

      if (movie.Id == 0)
      {
        _context.Movies.Add(movie);
      }

      else
      {
          var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
          movieInDb.Name = movie.Name;
          movieInDb.GenreId = movie.GenreId;
          movieInDb.NumberInStock = movie.NumberInStock;
          movieInDb.ReleaseDate = movie.ReleaseDate;
      }

      _context.SaveChanges();

      return RedirectToAction("Index", "Movies");
    }

    public ActionResult Details(int id)
    {
      var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

      if (movie == null)
        return HttpNotFound();

      return View(movie);
    }
  }
}
