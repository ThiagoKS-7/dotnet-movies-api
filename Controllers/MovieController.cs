using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Data.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateMovie([FromBody] CreateMoviesDto movieDto)
    {
        try
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
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

            return _context.Movies.OrderBy(x => x.Id).Skip(skip).Take(take);
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