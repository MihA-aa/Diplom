using System.Web.Mvc;
using Course.WEB.Models.Repositories;
using Course.WEB.Models.Entities;
using Course.WEB.Helpers;

namespace Course.WEB.Controllers
{
    public class DisciplineController : Controller
    {
        readonly EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public ActionResult Get(int? disciplineId)
        {
            if (disciplineId == null)
                return HttpNotFound();
            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
                return HttpNotFound();

            return View(discipline);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Discipline discipline)
        {
            if (discipline == null)
                return HttpNotFound();
            db.Disciplines.Create(discipline);
            db.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? disciplineId)
        {
            if (disciplineId == null)
                return HttpNotFound();
            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
                return HttpNotFound();
            if (!User.HasPermissionToRedact(discipline.CreatorId))
                return HttpNotFound();

            return View(discipline);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Discipline discipline)
        {
            if (discipline == null)
                return HttpNotFound();
            db.Disciplines.Update(discipline);
            db.Save();

            return RedirectToAction("Index");
        }
    }
}