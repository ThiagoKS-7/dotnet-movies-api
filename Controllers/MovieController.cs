using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();

    [HttpPost("")]
    public CreatedResult AddMovie([FromBody] Movie movie)
    {
        movies.Add(movie);
        Console.WriteLine(movie.Title + " - " + movie.Duration + " - " + movie.Genre);
        return Created("~Movie", movie);
    }

    [HttpGet]
    public List<Movie> Get()
    {
        return movies;
    }
}