using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Course.WEB.Models.Repositories
{
    public class UserRepository : IDisposable
    {
        private readonly ApplicationUserManager userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public UserRepository()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return db.Users.Include(x => x.Roles).ToList();
        }

        public List<AdminPanelViewModel> GetUsersInfo()
        {
            var users = GetAllUsers();
            List<AdminPanelViewModel> usersStatistics = new List<AdminPanelViewModel>();
            foreach (var user in users)
            {
                usersStatistics.Add(
                    new AdminPanelViewModel()
                    {
                        User = user,
                        IsAdmin = userManager.IsInRole(user.Id, "admin"),
                        IsSuperAdmin = userManager.IsInRole(user.Id, "superAdmin")
                    });
            }

            return usersStatistics;
        }

        public ApplicationUser GetUser(string userId)
        {
            return GetAllUsers().FirstOrDefault(x => x.Id == userId);
        }

        public bool IsUserExist(string userId)
        {
            return GetUser(userId) != null;
        }

        public void BlockUser(string userId, bool isBlock)
        {
            if (IsUserExist(userId))
            {
                EditUserRoles(userId, "blocked", isBlock);
            }
        }

        public void UpgradeUser(string userId)
        {
            var tempUser = GetUser(userId);
            tempUser.Roles.Clear();
            tempUser.Roles.Add(new IdentityUserRole() { RoleId = "admin" });
            tempUser.Roles.Add(new IdentityUserRole() { RoleId = "user" });
            db.SaveChanges();
        }

        public ApplicationUser GetUserByName(string userName)
        {
            return db.Users.Include("Sites.Tags")
                .Include("Comments")
                .Include("RateNotes")
                .FirstOrDefault(x => x.UserName == userName);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        private void EditUserRoles(string userId, string roleName, bool isAdd)
        {
            if (isAdd)
            {
                userManager.AddToRole(userId, roleName);
            }
            else
            {
                userManager.RemoveFromRole(userId, roleName);
            }
        }
    }
}