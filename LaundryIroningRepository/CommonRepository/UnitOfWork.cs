using LaundryIroningEntity.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LaundryIroningRepository.CommonRepository
{
    /// <summary>
    /// UnitOfWork class take cares of context and database changes
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Variables
        private readonly DbContext _context;
        private bool _disposed;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize DBContext.
        /// </summary>
        /// <param name="DbContext"></param>
        public UnitOfWork(DbContext context)
        {
            _context = context;
            _disposed = false;
            
        }
        #endregion

        #region Public Methods

        public int RejectChanges()
        {
            try
            {
                int counter = 0;
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                    counter++;
                }

                return counter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Save changes for entity db synchronously
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save changes for entity db asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
              
        public DbContext DataContext
        {
            get { return _context; }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Method to dispose by parameter.
        /// </summary>
        /// <param name="disposing"></param>
        /// 
        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }          
              
            }

            _disposed = true;
        }

        /// <summary>
        /// Method to dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
