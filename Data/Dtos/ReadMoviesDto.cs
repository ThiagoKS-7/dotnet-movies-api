using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Data.Dtos;

public class ReadMoviesDto
{
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int Duration { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime SearchedAt { get; set; } = DateTime.Now;
}