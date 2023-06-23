using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();
    public void AddMovie(Movie movie)
    {
        movies.Add(movie);
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public void Get()
    {

    }
}