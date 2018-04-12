using Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.WEB.Models.Repositories
{
    public class TopicRepository : GenericRepository<Topic>
    {
        public TopicRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}