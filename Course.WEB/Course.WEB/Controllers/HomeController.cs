using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Course.WEB.Models;
using Course.WEB.Models.MyViewModel;

namespace Course.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFUnitOfWork db = new EFUnitOfWork();

        public ActionResult Index()
        {
            var viewModel = new HomePageViewModel
            {
                Disciplines = db.Disciplines.GetAll().OrderBy(x => x.Order).ToList(),
                CountOfCourses = db.Courses.Count(),
                CountOfTopics = db.Topics.Count(),
                CountOfTasks = db.Tasks.Count(),
            };

            return View(viewModel);
        }

        public ActionResult Show(string item)
        {
            Dictionary<string, string>[] dictProperties;
            switch (item)
            {
                case "Courses":
                    dictProperties = new Dictionary<string, string>[db.Courses.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Courses.GetAll().ToList().Count(); i++)
                    {
                        dictProperties[i] = db.Courses.GetAll().ToList()[i].GetProperties();
                    }

                    break;
                case "Disciplines":
                    dictProperties = new Dictionary<string, string>[db.Disciplines.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Disciplines.GetAll().ToList().Count(); i++)
                    {
                        dictProperties[i] = db.Disciplines.GetAll().ToList()[i].GetProperties();
                    }

                    break;
                case "Topics":
                    dictProperties = new Dictionary<string, string>[db.Topics.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Topics.GetAll().ToList().Count(); i++)
                    {
                        dictProperties[i] = db.Topics.GetAll().ToList()[i].GetProperties();
                    }

                    break;
                case "Tasks":
                    dictProperties = new Dictionary<string, string>[db.Tasks.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Tasks.GetAll().ToList().Count(); i++)
                    {
                        dictProperties[i] = db.Tasks.GetAll().ToList()[i].GetProperties();
                    }

                    break;
                default:
                    dictProperties = new Dictionary<string, string>[db.Tasks.GetAll().ToList().Count()];
                    for (int i = 0; i < db.Tasks.GetAll().ToList().Count(); i++)
                    {
                        dictProperties[i] = db.Tasks.GetAll().ToList()[i].GetProperties();
                    }

                    break;
            }

            ViewBag.table = item;

            return View(dictProperties);
        }
    }
}