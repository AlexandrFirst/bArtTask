using System;
using System.Threading.Tasks;

namespace testWork.Abstractions
{
    public interface IRepositry<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<T> Read(Guid id);
    }
}