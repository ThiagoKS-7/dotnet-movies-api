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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="movieDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    /// <summary>
    /// Lista os filmes no banco de dados, ordenando por ID
    /// </summary>
    /// <param name="skip">Inteiro que corresponde a quantos filmes o sistema deve pular na consulta</param>
    /// <param name="take">Inteiro que corresponde a quantos filmes o sistema deve trazer na consulta</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso listagem seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadMoviesDto> ListMovies([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        try
        {

            return _mapper.Map<List<ReadMoviesDto>>(_context.Movies.OrderBy(x => x.Id).Skip(skip).Take(take));
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// Lista os dados de um filme no banco de dados, a partir do ID
    /// </summary>
    /// <param name="id">Inteiro que corresponde ao ID de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso filme seja encontrado com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetMovieById(int id)
    {
        try
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (foundMovie == null) return NotFound();
            var movieDto = _mapper.Map<ReadMoviesDto>(foundMovie);
            return Ok(movieDto);
        }
        catch (Exception e)
        {
            throw e;
        }

    }


    /// <summary>
    /// Atualiza os dados de um filme no banco de dados, a partir do ID
    /// </summary>
    /// <param name="id">Inteiro que corresponde ao ID de um filme</param>
    /// <param name="movieDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso filme seja atualizado com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

    /// <summary>
    /// Atualiza um dado específico de um filme no banco de dados, a partir do ID
    /// </summary>
    /// <param name="id">Inteiro que corresponde ao ID de um filme</param>
    /// <param name="patch">Lista de objetos com {op: operação, path: nome do campo, value: alteração}</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso filme seja atualizado com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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


    /// <summary>
    /// Deleta um filme específico no banco de dados, a partir do ID
    /// </summary>
    /// <param name="id">Inteiro que corresponde ao ID de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso filme seja removido com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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