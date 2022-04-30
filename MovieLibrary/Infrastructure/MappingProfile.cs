using AutoMapper;
using MovieLibrary.Data.Models;
using MovieLibrary.Models;
using MovieLibrary.Models.Movies;
using MovieLibrary.Services.Actors;
using MovieLibrary.Services.Movies;

namespace MovieLibrary.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Genre, MovieGenreServiceModel>();
            this.CreateMap<Movie, MovieServiceModel>()
                .ForMember(m => m.GenreName, cfg => cfg.MapFrom(m => m.Genre.Name));
            this.CreateMap<Movie, LatestMoviesServiceModel>()
                .ForMember(m => m.GenreName, cfg => cfg.MapFrom(m => m.Genre.Name));

            this.CreateMap<MovieDetailsServiceModel, MovieFormModel>();

            this.CreateMap<Actor, ActorServiceModel>();

            this.CreateMap<Movie, MovieDetailsServiceModel>()
                .ForMember(m=> m.UserId, cfg => cfg.MapFrom(m=> m.TicketSeller.UserId))
                .ForMember(m=> m.GenreName, cfg=> cfg.MapFrom(m=> m.Genre.Name));


        }
    }
}
