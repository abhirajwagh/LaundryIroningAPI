using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LaundryIroningEntity.Contract
{
    // <summary>
    /// Interface for the UnitOfWork class.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();

        DbContext DataContext { get; }
    }
}
