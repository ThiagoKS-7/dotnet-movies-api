using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Data.Dtos;

public class UpdateMoviesDto
{

    [Required(ErrorMessage = "O título é obrigatório")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório")]
    [StringLength(50, ErrorMessage = "Tamanho não pode ser maior que 50 caracteres")]
    public string Genre { get; set; }

    [Required]
    [Range(1, 600, ErrorMessage = "Duração deve ter entre 1 e 600 minutos")]
    public int Duration { get; set; }

    public DateTime CreatedAt { get; set; }

    public UpdateMoviesDto()
    {
        Title = "";
        Genre = "";
        CreatedAt = DateTime.UtcNow;
    }
}