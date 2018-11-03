using System.Web.Mvc;
using Course.WEB.Helpers;
using Course.WEB.Models;
using Course.WEB.Models.Entities;

namespace Course.WEB.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public ActionResult Get(int? disciplineId)
        {
            if (disciplineId == null)
            {
                return HttpNotFound();
            }

            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
            {
                return HttpNotFound();
            }

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
            {
                return HttpNotFound();
            }

            db.Disciplines.Create(discipline);
            db.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? disciplineId)
        {
            if (disciplineId == null)
            {
                return HttpNotFound();
            }

            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
            {
                return HttpNotFound();
            }

            if (!User.HasPermissionToRedact(discipline.CreatorId))
            {
                return HttpNotFound();
            }

            return View(discipline);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Discipline discipline)
        {
            if (discipline == null)
            {
                return HttpNotFound();
            }

            db.Disciplines.Update(discipline);
            db.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(int? disciplineId)
        {
            if (disciplineId == null)
            {
                return HttpNotFound();
            }

            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
            {
                return HttpNotFound();
            }

            if (!User.HasPermissionToRedact(discipline.CreatorId))
            {
                return HttpNotFound();
            }

            db.Disciplines.Delete(disciplineId.Value);
            db.Save();

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}