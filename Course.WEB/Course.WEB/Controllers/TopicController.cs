using System.Linq;
using System.Web.Mvc;
using Course.WEB.Helpers;
using Course.WEB.Models;
using Course.WEB.Models.Entities;
using Course.WEB.Models.MyViewModel;

namespace Course.WEB.Controllers
{
    public class TopicController : Controller
    {
        private readonly EFUnitOfWork db = new EFUnitOfWork();

       [HttpGet]
        public ActionResult Get(int? topicId)
        {
            if (topicId == null)
            {
                return HttpNotFound();
            }

            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
            {
                return HttpNotFound();
            }

            var taskIds = topic.Tasks.Select(x => x.Id);
            var topicStatistic = db.TopicStatistics.Find(x => x.TopicId == topicId).FirstOrDefault();
            var tasksStatistic = db.TaskStatistics.Find(x => taskIds.Contains(x.TaskId)).ToList();

            var viewModel = new TopicViewModel
            {
                Topic = topic,
                TopicStatistic = topicStatistic,
                TasksStatistic = tasksStatistic
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int? courseId)
        {
            if (courseId == null)
            {
                return HttpNotFound();
            }

            var course = db.Courses.Get(courseId.Value);
            if (course == null)
            {
                return HttpNotFound();
            }

            ViewBag.Course = course.Name;
            ViewBag.CourseId = course.Id;

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Topic topic)
        {
            if (topic == null)
            {
                return HttpNotFound();
            }

            db.Topics.Create(topic);
            db.Save();

            return RedirectToAction("Get", "Course", new { courseId = topic.CourseId });
        }

        [HttpPost]
        public ActionResult Delete(int? topicId)
        {
            if (topicId == null)
            {
                return HttpNotFound();
            }

            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
            {
                return HttpNotFound();
            }

            if (User.HasPermissionToRedact(topic.CreatorId))
            {
                return HttpNotFound();
            }

            db.Topics.Delete(topicId.Value);
            db.Save();

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? topicId)
        {
            if (topicId == null)
            {
                return HttpNotFound();
            }

            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
            {
                return HttpNotFound();
            }

            if (!User.HasPermissionToRedact(topic.CreatorId))
            {
                return HttpNotFound();
            }

            return View(topic);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Topic topic)
        {
            if (topic == null)
            {
                return HttpNotFound();
            }

            db.Topics.Update(topic);
            db.Save();

            return RedirectToAction("Get", "Course", new { courseId = topic.CourseId });
        }
    }
}