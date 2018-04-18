using Course.WEB.Models.Repositories;
using System.Web.Mvc;
using de = Course.WEB.Models.Entities;
using System.Linq;
using Course.WEB.Helpers;
using Course.WEB.Models.MyViewModel;

namespace Course.WEB.Controllers
{
    public class CourseController : Controller
    {
        readonly EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public ActionResult Get(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();

            var topicIds = course.Topics.Select(x => x.Id);
            var topicsStatistic = db.TopicStatistics.Find(x => topicIds.Contains(x.TopicId)).ToList();
            var viewModel = new CourseViewModel
            {
                Course = course,
                TopicsStatistic = topicsStatistic
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int? disciplineId)
        {
            if (disciplineId == null)
                return HttpNotFound();
            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
                return HttpNotFound();
            ViewBag.Discipline = discipline.Name;
            ViewBag.DisciplineId = discipline.Id;

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(de.Course course)
        {
            if (course == null)
                return HttpNotFound();
            db.Courses.Create(course);
            db.Save();

            return RedirectToAction("Get", "Discipline", new { disciplineId = course.DisciplineId });
        }

        [HttpPost]
        public ActionResult Delete(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();
            if (!User.HasPermissionToRedact(course.CreatorId))
                return HttpNotFound();
            db.Courses.Delete(courseId.Value);
            db.Save();

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();
            if (!User.HasPermissionToRedact(course.CreatorId))
                return HttpNotFound();

            return View(course);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(de.Course course)
        {
            if (course == null)
                return HttpNotFound();
            db.Courses.Update(course);
            db.Save();

            return RedirectToAction("Get", "Discipline", new { disciplineId = course.DisciplineId });
        }
    }
}