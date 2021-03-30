using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Repository
{
   public interface IIroningLaundryRepository: IBaseRepository<IroningLaundryOrder>, ISelectService<IroningLaundryOrder>, ISelectFirstOrDefaultService<IroningLaundryOrder>
    {
        IUnitOfWork Uow
        {
            get; set;
        }

        Task<IroningLaundryOrderViewModel> GetIroningLaundryOrderAsync(int orderId);

        Task<List<GetIroningLaundryOrdersForAdmin>> GetIroningLaundryOrdersForAdminAsync();
    }
}
