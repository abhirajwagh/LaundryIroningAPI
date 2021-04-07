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
   public interface IIroningRepository: IBaseRepository<IroningOrder>, ISelectService<IroningOrder>, ISelectFirstOrDefaultService<IroningOrder>
    {
        IUnitOfWork Uow
        {
            get; set;
        }

        Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId);
        Task<List<GetIroningOrdersForAdmin>> GetIroningOrdersForAdminAsync();
        Task<List<GetAllOrdersForAgentOperator>> GetAllNewOrdersForAgentAsync(Guid? agentId);
        Task<List<GetAllOrdersForAgentOperator>> GetAllProcessedOrdersForAgentAsync(Guid? agentId);
        Task<List<GetAllOrdersForAgentOperator>> GetAllPickedOrdersForOperatorAsync(Guid? operatorId);
        Task<int> UpdateOrderStatusAsync(string orderNo, string orderType, string orderStatus);
        Task<List<GetAgentOrderAssignmentCount>> GetAgentOrdersAssignmentCountAsync();
    }
}
