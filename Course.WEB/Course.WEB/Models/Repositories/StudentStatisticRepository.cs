using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class StudentStatisticRepository : GenericRepository<StudentStatistic>
    {
        public StudentStatisticRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}