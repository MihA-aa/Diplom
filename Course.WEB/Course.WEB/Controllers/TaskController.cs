using System.Web.Mvc;
using Course.WEB.Helpers;
using Course.WEB.Models.Repositories;
using Course.WEB.Models.Entities;
using Microsoft.AspNet.Identity;
using System;

namespace Course.WEB.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        public EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public ActionResult Create(int? topicId)
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
        public ActionResult Create(Task task)
        {
            if (task == null)
                return HttpNotFound();
            db.Tasks.Create(task);
            db.Save();

            return RedirectToAction("Get", "Topic", new { topicId = task.TopicId });
        }

        [HttpGet]
        public ActionResult Edit(int? taskId)
        {
            if (taskId == null)
                return HttpNotFound();
            var task = db.Tasks.Get(taskId.Value);
            if (task == null)
                return HttpNotFound();
            if (!User.HasPermissionToRedact(task.CreatorId))
                return HttpNotFound();

            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (task == null)
                return HttpNotFound();
            db.Tasks.Update(task);
            db.Save();

            return RedirectToAction("Get", "Topic", new { topicId = task.TopicId });
        }

        [HttpPost]
        public ActionResult Delete(int? taskId)
        {
            if (taskId == null)
                return HttpNotFound();
            var task = db.Tasks.Get(taskId.Value);
            if (task == null)
                return HttpNotFound();
            if (!User.HasPermissionToRedact(task.CreatorId))
                return HttpNotFound();
            db.Tasks.Delete(taskId.Value);
            db.Save();

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult Solve(int? taskId)
        {
            if (taskId == null)
                return HttpNotFound();
            var task = db.Tasks.Get(taskId.Value);
            if (task == null)
                return HttpNotFound();

            return View(task);
        }

        [HttpPost]
        public ActionResult Solve(Task task, string time)
        {
            if (task == null)
                return HttpNotFound();
            var oldtask = db.Tasks.Get(task.Id);
            if (oldtask == null)
                return HttpNotFound();

            var isSolved = task.Answer == oldtask.Answer;
            var timeInSecond = time.ConvertStringTimeToInt();
            var rating = new Rating
            {
                TaskId = oldtask.Id,
                StudentId = User.Identity.GetUserId(),
                ActualTime = timeInSecond,
                DateOfSolution = DateTime.Now,
                IsSolved = isSolved
            };
            db.Ratings.Create(rating);
            db.Save();

            TempData["message"] = string.Format("Задача \"{0}\" была решена " + (isSolved ? "" : "не") + "верно, ваше время: {1} секунд", oldtask.Name, timeInSecond);
            TempData["resolve"] = isSolved;

            return RedirectToAction("Get", "Topic", new { topicId = oldtask.TopicId });
        }
    }
}