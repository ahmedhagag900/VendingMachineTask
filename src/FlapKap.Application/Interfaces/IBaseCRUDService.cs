using FlapKap.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.Interfaces
{
    public interface IBaseCRUDService<T> where T : BaseModel
    {
        Task<T> Add(T model);
        Task<T> Update(T model);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
