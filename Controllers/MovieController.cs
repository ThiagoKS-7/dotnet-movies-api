using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpPut("{id}")]
    public IActionResult UpdateMovieById(int id, [FromBody] UpdateMoviesDto movieDto)
    {
        try
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (foundMovie == null) return NotFound();
            _mapper.Map(movieDto, foundMovie);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    [HttpPatch("{id}")]
    public IActionResult UpdateMovieFieldById(int id, JsonPatchDocument<UpdateMoviesDto> patch)
    {
        try
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (foundMovie == null) return NotFound();

            var movieToUpdate = _mapper.Map<UpdateMoviesDto>(foundMovie);
            patch.ApplyTo(movieToUpdate, ModelState);
            if (!TryValidateModel(movieToUpdate)) return ValidationProblem(ModelState);
            _mapper.Map(movieToUpdate, foundMovie);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        try
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (foundMovie == null) return NotFound();
            _context.Remove(foundMovie);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception e)
        {
            throw e;
        }

    }
}