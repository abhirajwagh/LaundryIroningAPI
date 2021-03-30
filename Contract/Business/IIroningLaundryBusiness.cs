using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Business
{
   public interface IIroningLaundryBusiness
    {
        IUnitOfWork Uow { get; set; }

        Task<List<IroningLaundryOrder>> GetAllIroningLaundryOrderAsync();
        Task<IroningLaundryOrderViewModel> GetIroningLaundryOrderAsync(int orderId);
        Task<int> AddIroningLaundryOrderAsync(IroningLaundryOrder order);
        Task<List<GetIroningLaundryOrdersForAdmin>> GetIroningLaundryOrdersForAdminAsync();
    }
}
