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
    public CreatedResult CreateMovie([FromBody] Movie movie)
    {
        movie.Id = id++;
        movies.Add(movie);
        return Created("~Movie", movie);
    }

    [HttpGet]
    public IEnumerable<Movie> ListMovies([FromQuery] int skip, [FromQuery] int take)
    {
        return movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public Movie? GetMovie(int id)
    {
        System.Console.WriteLine(id);
        return movies.FirstOrDefault(movie => movie.Id == id);
    }
}