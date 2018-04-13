using de = Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class CourseRepository : GenericRepository<de.Course>
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}