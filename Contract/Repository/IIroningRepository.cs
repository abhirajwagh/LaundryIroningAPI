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
   public interface IIroningRepository: IBaseRepository<IroningOrder>, ISelectService<IroningOrder>, ISelectFirstOrDefaultService<IroningOrder>
    {
        IUnitOfWork Uow
        {
            get; set;
        }

        Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId);
    }
}
