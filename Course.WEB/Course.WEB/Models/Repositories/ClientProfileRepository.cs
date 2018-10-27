namespace Course.WEB.Models.Repositories
{
    public class ClientProfileRepository : GenericRepository<ApplicationUser>
    {
        public ClientProfileRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}