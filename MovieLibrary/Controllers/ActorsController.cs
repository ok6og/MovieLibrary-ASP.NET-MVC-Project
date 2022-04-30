using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Models;
using MovieLibrary.Services.Actors;

namespace MovieLibrary.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService data;

        public ActorsController(IActorsService actorsService)
        {
            data = actorsService;
        }

        public IActionResult Index()
        {
            var actorData = data.GetAll();
            return View(actorData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ActorFormModel actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            

            data.Add(actor);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var actorDetails = data.GetById(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var actorDetails = data.GetById(Id);
            
            if (actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }

        [HttpPost]
        public IActionResult Edit(int id,Actor actor)
        {
            data.Update(id,actor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var actorDetails = data.GetById(Id);

            if (actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var actorDetails = data.GetById(id);
            if (actorDetails == null) return View("NotFound");

            data.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
