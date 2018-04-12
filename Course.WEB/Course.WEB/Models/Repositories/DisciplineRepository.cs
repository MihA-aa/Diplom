using Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.WEB.Models.Repositories
{
    public class DisciplineRepository : GenericRepository<Discipline>
    {
        public DisciplineRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}