using System.Web.Mvc;

namespace Course.WEB.Controllers
{
    public class GraphicController : Controller
    {
        [HttpGet]
        public void Get()
        {
            Response.Redirect("http://localhost:9847/Scripts/Graphics/index.html");
        }
    }
}