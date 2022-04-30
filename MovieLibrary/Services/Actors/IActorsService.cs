using MovieLibrary.Data.Models;
using MovieLibrary.Models;

namespace MovieLibrary.Services.Actors
{
    public interface IActorsService
    {
        IEnumerable<Actor> GetAll();
        Actor GetById(int id);
        void Add(ActorFormModel actor);
        Actor Update(int id, Actor newActor);
        void Delete(int id);
    }
}
