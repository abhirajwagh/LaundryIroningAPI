using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Business
{
   public interface ILaundryBusiness
    {
        IUnitOfWork Uow { get; set; }

        Task<List<LaundryOrder>> GetAllLaundryOrderAsync();
        Task<LaundryOrderViewModel> GetLaundryOrderAsync(int orderId);
        Task<int> AddLaundryOrderAsync(LaundryOrder order);
    }
}
