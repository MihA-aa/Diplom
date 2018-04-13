using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Course.WEB.Models.Repositories
{
    public class UserRepository : IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;
        public UserRepository()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
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
            if (GetUser(userId) != null)
                return true;
            return false;
        }

        private void EditUserRoles(string userId, string roleName, bool isAdd)
        {
            if (isAdd)
                userManager.AddToRole(userId, roleName);
            else
                userManager.RemoveFromRole(userId, roleName);
        }

        public void BlockUser(string userId, bool isBlock)
        {
            if (IsUserExist(userId))
                EditUserRoles(userId, "blocked", isBlock);
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
        
        //public IndexViewModel GetUserInfo(string user_id)
        //{
        //    var user = GetUser(user_id);
        //    if (user != null)
        //    {
        //        var model = new IndexViewModel
        //        {
        //            UserName = user.UserName,
        //            Avatar = user.Avatar,
        //            RegistrutionDate = user.RegistrationDate,
        //            UserRating = user.Rating,
        //            UserInstructionsCount = user.Instructions.Count,
        //            UserCommentsCount = user.Comments.Count,
        //            UserLikesCount = GetUserVotes(user_id, 1),
        //            UserDisLikesCount = GetUserVotes(user_id, -1)
        //        };
        //        return model;
        //    }
        //    return null;
        //}
    }
}