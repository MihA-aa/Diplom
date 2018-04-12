using Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.WEB.Models.Repositories
{
    public class ClientProfileRepository : GenericRepository<ApplicationUser>
    {
        public ClientProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}