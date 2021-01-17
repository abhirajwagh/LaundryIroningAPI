using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace LaundryIroningContract.Infrastructure
{
    public interface IAddComplexQuery<TEntity> where TEntity : class
    {
        Task<int> AddComplexDataAsync(List<TEntity> complexdata);
    }
    public interface ISelectComplexDataById<TEntity> where TEntity : class
    {
        Task<object> GetComplexDataById(int id);
    }
    public interface ISelectGuid<TEntity> where TEntity : class
    {
        Task<object> GetComplexDataByIdAsync(Guid id);
    }
    public interface IDeleteComplexList<TEntity> where TEntity : class
    {
        Task<bool> DeleteComplexListAsync(List<TEntity> complexList);
    }

    public interface IPostComplexListAsync<TEntity> where TEntity : class
    {
        Task<int> PostComplexListAsync(List<TEntity> complexList);
    }
}