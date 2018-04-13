using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class DisciplineRepository : GenericRepository<Discipline>
    {
        public DisciplineRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}