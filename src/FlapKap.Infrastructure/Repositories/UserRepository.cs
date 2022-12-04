using VendingMachine.Core.Entities;
using VendingMachine.Core.Repositories;

namespace VendingMachine.Infrastructure.Repositories
{
    internal class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(VendingMachieneContext context):base(context)
        {

        }
    }
}
