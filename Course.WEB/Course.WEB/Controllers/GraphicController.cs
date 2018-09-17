using Course.WEB.Models.Repositories;
using System.Web.Mvc;
using System.Linq;
using Course.WEB.Models.MyViewModel;

namespace Course.WEB.Controllers
{
    public class GraphicController : Controller
    {
        readonly EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public void Get()
        {
            Response.Redirect("http://localhost:9847/Scripts/Graphics/index.html");
        }
    }
}