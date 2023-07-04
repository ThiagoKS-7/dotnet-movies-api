using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();
    private static int id = 0;

    [HttpPost]
    public IActionResult CreateMovie([FromBody] Movie movie)
    {
        try
        {
            movie.Id = id++;
            movies.Add(movie);
            return Created("~Movie", movie);
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

            return movies.Skip(skip).Take(take);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetMovie(int id)
    {
        try
        {
            var foundMovie = movies.FirstOrDefault(movie => movie.Id == id);
            if (foundMovie != null) return Ok(foundMovie);
            return NotFound();
        }
        catch (Exception e)
        {
            throw e;
        }

    }
}