using FlapKap.Core.Entities;
using FlapKap.Core.Repositories;

namespace FlapKap.Infrastructure.Repositories
{
    internal class RoleRepository:BaseRepository<Role>,IRoleRepository
    {
        public RoleRepository(VendingMachieneContext context):base(context)
        {

        }
    }
}
