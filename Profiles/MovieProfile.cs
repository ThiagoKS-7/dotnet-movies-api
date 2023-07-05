using AutoMapper;
using MoviesApi.Models;
using MoviesApi.Data.Dtos;

namespace MoviesApi.Profiles;

public class MoviesProfile : Profile
{
    public MoviesProfile()
    {
        CreateMap<CreateMoviesDto, Movie>();
        CreateMap<UpdateMoviesDto, Movie>();
        CreateMap<Movie, UpdateMoviesDto>();
    }
}