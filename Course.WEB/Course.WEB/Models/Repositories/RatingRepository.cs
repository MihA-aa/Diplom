using Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.WEB.Models.Repositories
{
    public class RatingRepository : GenericRepository<Rating>
    {
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}