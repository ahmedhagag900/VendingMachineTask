using FlapKap.Core.Entities;
using FlapKap.Core.Repositories;

namespace FlapKap.Infrastructure.Repositories
{
    internal class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(VendingMachieneContext context):base(context)
        {

        }
    }
}
