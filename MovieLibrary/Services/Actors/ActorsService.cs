using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Models;

namespace MovieLibrary.Services.Actors
{
    public class ActorsService : IActorsService
    {
        private readonly MovieLibraryDbContext data;
        private readonly IMapper mapper;

        public ActorsService(MovieLibraryDbContext data) => this.data = data;

        public void Add(ActorFormModel actor)
        {

            var actorData = new Actor
            {
                ProfilePicture = actor.ProfilePicture,
                Bio = actor.Bio,
                FullName = actor.FullName,
            };
            data.Actors.Add(actorData);
            data.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = data.Actors.FirstOrDefault(a => a.Id == id);
            data.Actors.Remove(result);
            data.SaveChanges();
        }

        public IEnumerable<Actor> GetAll()
        {
            var result = data.Actors.ToList();
            return result;
        }

        public Actor GetById(int id)
        {
            var result = data.Actors.FirstOrDefault(n=> n.Id == id);
            return result;
        }

        public Actor Update(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            data.Update(actor);
            data.SaveChanges();
            return actor;
        }

        //public IList<ActorServiceModel> All()
        //{
        //    var dataActor = GetActors();
        //    return dataActor;


        //}
        //private IEnumerable<ActorServiceModel> GetActors(IQueryable<Actor> actorQuery)
        //   => actorQuery
        //       .ProjectTo<ActorServiceModel>(this.mapper.ConfigurationProvider)
        //        .ToList();

    }
}
