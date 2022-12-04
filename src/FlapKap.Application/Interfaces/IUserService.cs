using VendingMachine.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Interfaces
{
    public interface IUserService:IBaseCRUDService<UserModel>
    {
        Task<LoginModel> LoginAsync(string userName, string password);
        Task<DepositModel> AddDeposit(double amount);
        Task<DepositModel> ResetDeposit();
    }
}
