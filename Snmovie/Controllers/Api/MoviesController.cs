using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Snmovie.Models;
using Snmovie.Dtos;
using AutoMapper;

namespace Snmovie.Controllers.Api
{
  public class MoviesController : ApiController
  {
    private ApplicationDbContext _context;

    public MoviesController()
    {
      _context = new ApplicationDbContext();
    }

    // GET  /Api/movies
    public IEnumerable<MovieDto> GetMovies()
    {
      return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
    }

    // GET /Api/movies/1

    public IHttpActionResult GetMovies(int id)
    {
      var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

      if (movie == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      return Ok(Mapper.Map<Movie, MovieDto>(movie));
    }

    // POST  /Api/movies // Create movie like this.
    [HttpPost]
    public IHttpActionResult CreateMovie(MovieDto movieDto)
    {
      if (!ModelState.IsValid)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      var movie = Mapper.Map<MovieDto, Movie>(movieDto);

      _context.Movies.Add(movie);
      _context.SaveChanges();

      movieDto.Id = movie.Id;

      return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
    }

    // PUT   /Api/customers/1   //Retunere
    [HttpPut]
    public IHttpActionResult UpdateMovie(int Id, MovieDto movieDto)
    {
      if (!ModelState.IsValid)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == Id);

      if (movieInDb == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      Mapper.Map(movieDto, movieInDb);

      _context.SaveChanges();

      return Ok();
    }

    // DELETE /Api/movies/1  // Slette 
    [HttpDelete]
    public IHttpActionResult DeleteMovie(int Id)
    {

      // først skal vi tjekke ind i databasen om kunden eksistere 
      var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == Id);

      if (movieInDb == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      _context.Movies.Remove(movieInDb);
      _context.SaveChanges();

      return Ok();
    }
  }
}
