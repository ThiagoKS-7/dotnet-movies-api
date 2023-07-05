using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;

    public MovieController(MovieContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateMovie([FromBody] Movie movie)
    {
        try
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(
                nameof(GetMovieById),
                new { id = movie.Id },
                movie
            );
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [HttpGet]
    public IEnumerable<Movie> ListMovies([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        try
        {

            return _context.Movies.Skip(skip).Take(take);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        try
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (foundMovie != null) return Ok(foundMovie);
            return NotFound();
        }
        catch (Exception e)
        {
            throw e;
        }

    }
}