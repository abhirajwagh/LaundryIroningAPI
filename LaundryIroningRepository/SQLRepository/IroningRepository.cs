using LaundryIroningCommon;
using LaundryIroningContract.Infrastructure;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using LaundryIroningHelper;
using LaundryIroningRepository.CommonRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningRepository.SQLRepository
{
   public class IroningRepository: BaseRepository<IroningOrder>, IIroningRepository
    {
        protected readonly IExecuterStoreProc _executerStoreProc;

        #region Public Variables

        public IroningRepository(IExecuterStoreProc executerStoreProc)
        {
            _executerStoreProc = executerStoreProc;
            Uow = _executerStoreProc.uow;
        }
        public IUnitOfWork Uow
        {
            get { return base.UnitOfWork; }
            set { base.UnitOfWork = value; }
        }


        public async Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("orderId", orderId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<IroningOrderViewModel>(ProcedureConstants.GetIroningOrderDetailsById, param))[0];
        }

        public async Task<List<GetIroningOrdersForAdmin>> GetIroningOrdersForAdminAsync()
        {
            return (await _executerStoreProc.ExecuteProcAsync<GetIroningOrdersForAdmin>(ProcedureConstants.GetIroningOrdersForAdmin));
        }

        public async Task<List<GetAllOrdersForAgentOperator>> GetAllNewOrdersForAgentAsync(Guid? agentId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("AgentId", agentId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<GetAllOrdersForAgentOperator>(ProcedureConstants.GetAllNewOrdersForAgent,param));
        }

        public async Task<List<GetAllOrdersForAgentOperator>> GetAllProcessedOrdersForAgentAsync(Guid? agentId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("AgentId", agentId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<GetAllOrdersForAgentOperator>(ProcedureConstants.GetAllProcessedOrdersForAgent, param));
        }
        public async Task<List<GetAllOrdersForAgentOperator>> GetAllPickedOrdersForOperatorAsync(Guid? operatorId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("OperatorId", operatorId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<GetAllOrdersForAgentOperator>(ProcedureConstants.GetAllPickedOrdersForOperator, param));
        }

        public async Task<int> UpdateOrderStatusAsync(string orderNo, string orderType, string orderStatus)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("orderNo", orderNo),
                new Parameters("orderType", orderType),
                new Parameters("orderStatus", orderStatus)
            };
            return (await _executerStoreProc.ExceuteNonQueryAsync(ProcedureConstants.UpdateOrderStatusByAgentOperator, param));
        }

        public async Task<List<GetAgentOrderAssignmentCount>> GetAgentOrdersAssignmentCountAsync()
        {
            return (await _executerStoreProc.ExecuteProcAsync<GetAgentOrderAssignmentCount>(ProcedureConstants.GetAgentOrderAssignmentCount));
        }
        #endregion
    }
}
