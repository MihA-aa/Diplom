using System.Web.Mvc;
using Course.WEB.Models;

namespace Course.WEB.Controllers
{
    public class GraphicController : Controller
    {
        private EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public ActionResult Index(int taskId)
        {
            return Content("<script>window.location = 'http://localhost:9847/Scripts/Graphics/index.html';</script>");
        }

        [HttpGet]
        public ActionResult Get(int taskId)
        {
            var task = db.Tasks.Get(taskId).GraphicTask;
            return Json(task, JsonRequestBehavior.AllowGet);
        }
    }
}