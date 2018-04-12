using de = Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.WEB.Models.Repositories
{
    public class CourseRepository : GenericRepository<de.Course>
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}