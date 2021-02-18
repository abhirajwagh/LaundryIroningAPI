using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Repository
{
   public interface ILaundryRepository: IBaseRepository<LaundryOrder>, ISelectService<LaundryOrder>, ISelectFirstOrDefaultService<LaundryOrder>
    {
        IUnitOfWork Uow
        {
            get; set;
        }

        Task<LaundryOrderViewModel> GetLaundryOrderAsync(int orderId);
    }
}
