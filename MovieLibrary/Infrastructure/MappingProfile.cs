using AutoMapper;
using MovieLibrary.Data.Models;
using MovieLibrary.Models.Home;
using MovieLibrary.Models.Movies;
using MovieLibrary.Services.Movies;

namespace MovieLibrary.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Movie, MovieIndexViewModel>();
            this.CreateMap<MovieDetailsServiceModel, MovieFormModel>();

            this.CreateMap<Movie, MovieDetailsServiceModel>()
                .ForMember(m=> m.UserId, cfg => cfg.MapFrom(m=> m.TicketSeller.UserId));
        }
    }
}
