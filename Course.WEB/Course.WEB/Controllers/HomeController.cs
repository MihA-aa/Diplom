using Course.WEB.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using de = Course.WEB.Models.Entities;
using Course.WEB.Models.Entities;
using Microsoft.AspNet.Identity;
using Course.WEB.Models.MyViewModel;

namespace Course.WEB.Controllers
{
    public class HomeController : Controller
    {
        readonly EFUnitOfWork db = new EFUnitOfWork();

        public ActionResult Index()
        {
            var viewModel = new HomePageViewModel
            {
                Disciplines = db.Disciplines.GetAll().ToList(),
                CountOfCourses = db.Courses.Count(),
                CountOfTopics = db.Topics.Count(),
                CountOfTasks = db.Tasks.Count(),
            };

            return View(viewModel);
        }
        public ActionResult Show(string item)
        {
            Dictionary<string, string>[] DictProperties;
            switch (item)
            {
                case "Courses":
                    DictProperties = new Dictionary<string, string>[db.Courses.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Courses.GetAll().ToList().Count(); i++)
                        DictProperties[i] = db.Courses.GetAll().ToList()[i].GetProperties();
                    break;
                case "Disciplines":
                    DictProperties = new Dictionary<string, string>[db.Disciplines.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Disciplines.GetAll().ToList().Count(); i++)
                        DictProperties[i] = db.Disciplines.GetAll().ToList()[i].GetProperties();
                    break;
                case "Topics":
                    DictProperties = new Dictionary<string, string>[db.Topics.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Topics.GetAll().ToList().Count(); i++)
                        DictProperties[i] = db.Topics.GetAll().ToList()[i].GetProperties();
                    break;
                case "Tasks":
                    DictProperties = new Dictionary<string, string>[db.Tasks.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Tasks.GetAll().ToList().Count(); i++)
                        DictProperties[i] = db.Tasks.GetAll().ToList()[i].GetProperties();
                    break;
                default:
                    DictProperties = new Dictionary<string, string>[db.Tasks.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Tasks.GetAll().ToList().Count(); i++)
                        DictProperties[i] = db.Tasks.GetAll().ToList()[i].GetProperties();
                    break;
            }
            ViewBag.table = item;
            return View(DictProperties);
        }

        #region Task methods

        [Authorize]
        public ActionResult CreateTask(int? topicId)
        {
            if (topicId == null)
                return HttpNotFound();
            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
                return HttpNotFound();
            ViewBag.Topic = topic.Name;
            ViewBag.TopicId = topic.Id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateTask(Task task)
        {
           if (task == null)
                return HttpNotFound();
            db.Tasks.Create(task);
            db.Save();
            return RedirectToAction("ShowTopic", new { topicId = task.TopicId });
        }

        [Authorize]
        public ActionResult RedactTask(int? taskId)
        {
            if (taskId == null)
                return HttpNotFound();
            var task = db.Tasks.Get(taskId.Value);
            if (task == null)
                return HttpNotFound();
            if (!IsUserHasPermission(task.CreatorId))
                return HttpNotFound();
            return View(task);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RedactTask(Task task)
        {
            if (task == null)
                return HttpNotFound();
            db.Tasks.Update(task);
            db.Save();
            return RedirectToAction("ShowTopic", new { topicId = task.TopicId });
        }

        [HttpPost]
        public ActionResult DeleteTask(int? taskId)
        {
            if (taskId == null)
                return HttpNotFound();
            var task = db.Tasks.Get(taskId.Value);
            if (task == null)
                return HttpNotFound();
            if (!IsUserHasPermission(task.CreatorId))
                return HttpNotFound();
            db.Tasks.Delete(taskId.Value);
            db.Save();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult SolveTask(int? taskId)
        {
            if (taskId == null)
                return HttpNotFound();
            var task = db.Tasks.Get(taskId.Value);
            if (task == null)
                return HttpNotFound();
            return View(task);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SolveTask(Task task, string time)
        {
            if (task == null)
                return HttpNotFound();
            var oldtask = db.Tasks.Get(task.Id);
            if (oldtask == null)
                return HttpNotFound();
            #region
            var isSolved = task.Answer == oldtask.Answer;
            String[] substrings = time.Split(':');
            int sumTime = Convert.ToInt32(substrings[0]) * 3600;
            sumTime += Convert.ToInt32(substrings[1]) * 60;
            sumTime += Convert.ToInt32(substrings[2].Split('.')[0]);
            decimal actualComplexity;
            if (sumTime < oldtask.PlannedTime)
                actualComplexity = 1.0m;
            else if (sumTime > oldtask.PlannedTime * 2)
                actualComplexity = 10.0m;
            else
                actualComplexity = (decimal)sumTime * 10 / oldtask.PlannedTime;
            #endregion
            Rating rating = new Rating
            {
                TaskId = oldtask.Id,
                ApplicationUserId = User.Identity.GetUserId(),
                ActualTime = sumTime,
                ActualComplexity = actualComplexity,
                IsSolved = isSolved
            };
            db.Ratings.Create(rating);
            db.Save();
            if (isSolved)
            {
                TempData["message"] = string.Format("Задача \"{0}\" была решена верно, ваше время: {1} секунд"+
                    ", ваша сложность решения задачи: {2}", oldtask.Name, sumTime, actualComplexity);
                TempData["resolve"] = string.Format("True");
            }
            else
            {
                TempData["message"] = string.Format("Задача \"{0}\" была решена не верно, ваше время: {1} секунд", oldtask.Name, sumTime);
                TempData["resolve"] = string.Format("False");
            }
            return RedirectToAction("ShowTopic", new { topicId = oldtask.TopicId });
        }
        #endregion
        
        #region Topic methods

        public ActionResult ShowTopic(int? topicId)
        {
            if (topicId == null)
                return HttpNotFound();
            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
                return HttpNotFound();
            return View(topic);
        }

        [Authorize]
        public ActionResult CreateTopic(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();
            ViewBag.Course = course.Name;
            ViewBag.CourseId = course.Id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateTopic(Topic topic)
        {
            if (topic == null)
                return HttpNotFound();
            db.Topics.Create(topic);
            db.Save();
            return RedirectToAction("ShowCourse", new { courseId = topic.CourseId });
        }

        [HttpPost]
        public ActionResult DeleteTopic(int? topicId)
        {
            if (topicId == null)
                return HttpNotFound();
            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
                return HttpNotFound();
            if (IsUserHasPermission(topic.CreatorId))
                return HttpNotFound();
            db.Topics.Delete(topicId.Value);
            db.Save();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        
        [Authorize]
        public ActionResult RedactTopic(int? topicId)
        {
            if (topicId == null)
                return HttpNotFound();
            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
                return HttpNotFound();
            if (!IsUserHasPermission(topic.CreatorId))
                return HttpNotFound();
            return View(topic);
        }
        [HttpPost]
        [Authorize]
        public ActionResult RedactTopic(Topic topic)
        {
            if (topic == null)
                return HttpNotFound();
            db.Topics.Update(topic);
            db.Save();
            return RedirectToAction("ShowCourse", new { courseId = topic.CourseId });
        }
        #endregion

        #region Course methods

        public ActionResult ShowCourse(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();
            return View(course);
        }
        [Authorize]
        public ActionResult CreateCourse(int? disciplineId)
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
        public ActionResult CreateCourse(de.Course course)
        {
            if (course == null)
                return HttpNotFound();
            db.Courses.Create(course);
            db.Save();
            return RedirectToAction("ShowDiscipline", new { disciplineId = course.DisciplineId });
        }

        [HttpPost]
        public ActionResult DeleteCourse(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();
            if (!IsUserHasPermission(course.CreatorId))
                return HttpNotFound();
            db.Courses.Delete(courseId.Value);
            db.Save();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult RedactCourse(int? courseId)
        {
            if (courseId == null)
                return HttpNotFound();
            var course = db.Courses.Get(courseId.Value);
            if (course == null)
                return HttpNotFound();
            if (!IsUserHasPermission(course.CreatorId))
                return HttpNotFound();
            return View(course);
        }
        [HttpPost]
        [Authorize]
        public ActionResult RedactCourse(de.Course course)
        {
            if (course == null)
                return HttpNotFound();
            db.Courses.Update(course);
            db.Save();
            return RedirectToAction("ShowDiscipline", new { disciplineId = course.DisciplineId });
        }
        #endregion

        public ActionResult ShowDiscipline(int? disciplineId)
        {
            if (disciplineId == null)
                return HttpNotFound();
            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
                return HttpNotFound();
            return View(discipline);
        }

        [Authorize]
        public ActionResult CreateDiscipline()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateDiscipline(Discipline discipline)
        {
            if (discipline == null)
                return HttpNotFound();
            db.Disciplines.Create(discipline);
            db.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult RedactDiscipline(int? disciplineId)
        {
            if (disciplineId == null)
                return HttpNotFound();
            var discipline = db.Disciplines.Get(disciplineId.Value);
            if (discipline == null)
                return HttpNotFound();
            if (!IsUserHasPermission(discipline.CreatorId))
                return HttpNotFound();
            return View(discipline);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RedactDiscipline(Discipline discipline)
        {
            if (discipline == null)
                return HttpNotFound();
            db.Disciplines.Update(discipline);
            db.Save();
            return RedirectToAction("Index");
        }

        private bool IsUserHasPermission(string creatorId)
        {
            return creatorId == User.Identity.GetUserId() || User.IsInRole("superAdmin");
        }
    }
}