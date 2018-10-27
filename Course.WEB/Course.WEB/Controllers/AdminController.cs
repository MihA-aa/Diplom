using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course.WEB.Models;
using Course.WEB.Models.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Course.WEB.Controllers
{
    [Authorize(Roles = "superAdmin")]
    public class AdminController : Controller
    {
        private readonly UserRepository userRepository = new UserRepository();

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ActionResult Index()
        {
            return View(userRepository.GetUsersInfo());
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = userRepository.GetUser(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        [HttpPost]
        [ActionName("Upgrade")]
        public void Upgrade(string id)
        {
            UserManager.AddToRole(id, "admin");
        }

        [HttpPost]
        [ActionName("UpgradSuper")]
        public void UpgradSuper(string id)
        {
            UserManager.AddToRole(id, "superAdmin");
        }

        [HttpPost]
        [ActionName("LevelDown")]
        public void LevelDown(string id)
        {
            if (UserManager.IsInRole(id, "superAdmin"))
            {
                UserManager.RemoveFromRole(id, "superAdmin");
            }

            if (UserManager.IsInRole(id, "admin"))
            {
                UserManager.RemoveFromRole(id, "admin");
            }
        }

        [HttpPost]
        [ActionName("Block")]
        public void Block(string id)
        {
             UserManager.SetLockoutEnabledAsync(id, true);
             UserManager.SetLockoutEndDateAsync(id, DateTime.Today.AddYears(10));
        }

        [HttpPost]
        [ActionName("Unblock")]
        public async void Unblock(string id)
        {
            await UserManager.SetLockoutEnabledAsync(id, false);
        }
    }
}