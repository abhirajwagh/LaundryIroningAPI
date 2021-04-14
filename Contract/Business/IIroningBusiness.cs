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
   public interface IIroningBusiness
    {
        IUnitOfWork Uow { get; set; }

        Task<List<IroningOrder>> GetAllIroningOrderAsync();
        Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId);
        Task<int> AddIroningOrderAsync(IroningOrder order);
        Task<List<GetIroningOrdersForAdmin>> GetIroningOrdersForAdminAsync();
        Task<int> UpdateOrderAssignemnt(Guid agentId, List<string> OrderId);
        Task<List<GetAllOrdersForAgentOperator>> GetAllNewOrdersForAgentAsync(Guid? agentId);
        Task<List<GetAllOrdersForAgentOperator>> GetAllProcessedOrdersForAgentAsync(Guid? agentId);
        Task<List<GetAllOrdersForAgentOperator>> GetAllPickedOrdersForOperatorAsync(Guid? operatorId);
        Task<int> UpdateOrderStatusAsync(UpdateOrderStatusViewModel updateOrderStatusViewModel);
    }
}
