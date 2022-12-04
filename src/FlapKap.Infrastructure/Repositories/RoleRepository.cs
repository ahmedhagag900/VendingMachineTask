using VendingMachine.Core.Entities;
using VendingMachine.Core.Repositories;

namespace VendingMachine.Infrastructure.Repositories
{
    internal class RoleRepository:BaseRepository<Role>,IRoleRepository
    {
        public RoleRepository(VendingMachieneContext context):base(context)
        {

        }
    }
}
