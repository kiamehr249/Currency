using Currency.Core.Entities;
using Currency.Core.Repositories;

namespace Currency.Infrastructure.Repositories
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        public UserRepo(AppDbContext employeeContext) : base(employeeContext)
        {
        }
    }
}
