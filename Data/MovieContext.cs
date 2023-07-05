using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opts)
    : base(opts)
    {

    }

    public DbSet<Movie> Movies { get; set; }
}