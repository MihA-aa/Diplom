using Course.WEB.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Course.WEB.Models.Entities;
using Course.WEB.Models.MyViewModel;
using Course.WEB.Helpers;

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
    }
}